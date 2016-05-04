using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.Extensions;
using esperancephone.Helpers;
using esperancephone.Interfaces;
using Microsoft.WindowsAzure.MobileServices;

namespace esperancephone.Managers
{
    public class EsperancePhoneApiManager : IEsperancePhoneApiManager
    {
        private readonly MobileServiceClient _currentClient;

#if OFFLINE_SYNC_ENABLED
        private IMobileServiceSyncTable<TodoItem> todoTable;
#else
        private IMobileServiceTable<TodoItem> todoTable;
#endif

        public MobileServiceClient CurrentClient => _currentClient;

        public bool IsOfflineEnabled => todoTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<TodoItem>;

        public EsperancePhoneApiManager()
        {
            this._currentClient = new MobileServiceClient(Constants.ApplicationURL);

            CreateCurrentUser();

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore("localstore.db");
            store.DefineTable<TodoItem>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.todoTable = client.GetSyncTable<TodoItem>();
#else
            this.todoTable = this._currentClient.GetTable<TodoItem>();
#endif
        }

        private async void CreateCurrentUser()
        {
            if (this._currentClient.CurrentUser != null)
            {
                Settings.MobileServiceAuthenticationToken = this._currentClient.CurrentUser.MobileServiceAuthenticationToken;
            }

            if (this._currentClient.CurrentUser == null && !string.IsNullOrEmpty(Settings.UserId) &&
                !string.IsNullOrEmpty(Settings.MobileServiceAuthenticationToken))
            {
                this._currentClient.CurrentUser = new MobileServiceUser(Settings.UserId);
                this._currentClient.CurrentUser.MobileServiceAuthenticationToken = Settings.MobileServiceAuthenticationToken;
            }

        }

        public async Task SaveTaskAsync(TodoItem item)
        {
            if (item.Id == null)
            {
                await todoTable.InsertAsync(item);
            }
            else
            {
                await todoTable.UpdateAsync(item);
            }
        }

        public async Task<ObservableCollection<TodoItem>> GetTodoItemsAsync(bool syncItems = false)
        {
            Debug.WriteLine($"JWT: {this.todoTable.MobileServiceClient.CurrentUser.MobileServiceAuthenticationToken.GetJWT()}");
            //CreateCurrentUser();
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                IEnumerable<TodoItem> items = await this.todoTable
                    .Where(todoItem => !todoItem.Done)
                    .ToEnumerableAsync();

                return new ObservableCollection<TodoItem>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

                await this.todoTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allTodoItems",
                    this.todoTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
#endif
    }
}
