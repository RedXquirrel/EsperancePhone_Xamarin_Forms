using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Extensions;
using esperancephone.Helpers;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using Xamarin.Forms;

namespace esperancephone
{
    public partial class TodoList : ContentPage
    {
        bool authenticated = false;

        private IEsperancePhoneApiManager _apiManager;

        public TodoList()
        {
            InitializeComponent();

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                _apiManager = AppContainer.Container.Resolve<IEsperancePhoneApiManager>();
            }

            if(_apiManager != null) Debug.WriteLine($"INFORMATION: IEsperancePhoneApiManager loaded into TodoList ContentPage.");

            Debug.WriteLine($"INFORMATION: UserId = {Settings.UserId}");

            var loginButton = new Button
            {
                Text = "Login",
                TextColor = Xamarin.Forms.Color.Black,
                BackgroundColor = Xamarin.Forms.Color.Lime,
            };
            loginButton.Clicked += loginButton_Clicked;

            Xamarin.Forms.StackLayout bp = buttonsPanel as StackLayout;
            Xamarin.Forms.StackLayout bpParentStack = bp.Parent.Parent as StackLayout;

            bpParentStack.Padding = new Xamarin.Forms.Thickness(10, 30, 10, 20);
            bp.Orientation = StackOrientation.Vertical;
            bp.Children.Add(loginButton);

            // OnPlatform<T> doesn't currently support the "Windows" target platform, so we have this check here.
            //if (manager.IsOfflineEnabled &&
            //    (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone))
            //{
            //    var syncButton = new Button
            //    {
            //        Text = "Sync items",
            //        HeightRequest = 30
            //    };
            //    syncButton.Clicked += OnSyncItems;

            //    buttonsPanel.Children.Add(syncButton);
            //}
            if (_apiManager != null 
                && (_apiManager.IsOfflineEnabled 
                && (Device.OS == TargetPlatform.Windows 
                    || Device.OS == TargetPlatform.WinPhone)))
            {
                var syncButton = new Button
                {
                    Text = "Sync items",
                    HeightRequest = 30
                };
                syncButton.Clicked += OnSyncItems;

                buttonsPanel.Children.Add(syncButton);
            }
        }

        async void loginButton_Clicked(object sender, EventArgs e)
        {
            if (EsperancePhoneFormsApplication.Authenticator != null)
                authenticated = await EsperancePhoneFormsApplication.Authenticator.Authenticate();

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            if (authenticated == true)
                await RefreshItems(true, syncItems: false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            if (EsperancePhoneFormsApplication.Authenticated)
                await RefreshItems(true, syncItems: false);
        }

        // Data methods
        async Task AddItem(TodoItem item)
        {
            await _apiManager.SaveTaskAsync(item);
            todoList.ItemsSource = await _apiManager.GetTodoItemsAsync();
        }

        async Task CompleteItem(TodoItem item)
        {
            item.Done = true;
            await _apiManager.SaveTaskAsync(item);
            todoList.ItemsSource = await _apiManager.GetTodoItemsAsync();
        }

        public async void OnAdd(object sender, EventArgs e)
        {
            var todo = new TodoItem { Name = newItemName.Text };
            await AddItem(todo);

            newItemName.Text = string.Empty;
            newItemName.Unfocus();
        }

        // Event handlers
        public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var todo = e.SelectedItem as TodoItem;
            if (Device.OS != TargetPlatform.iOS && todo != null)
            {
                // Not iOS - the swipe-to-delete is discoverable there
                if (Device.OS == TargetPlatform.Android)
                {
                    await DisplayAlert(todo.Name, "Press-and-hold to complete task " + todo.Name, "Got it!");
                }
                else
                {
                    // Windows, not all platforms support the Context Actions yet
                    if (await DisplayAlert("Mark completed?", "Do you wish to complete " + todo.Name + "?", "Complete", "Cancel"))
                    {
                        await CompleteItem(todo);
                    }
                }
            }

            // prevents background getting highlighted
            todoList.SelectedItem = null;
        }

        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#context
        public async void OnComplete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var todo = mi.CommandParameter as TodoItem;
            await CompleteItem(todo);
        }

        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#pulltorefresh
        public async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;
            try
            {
                await RefreshItems(false, true);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }

            if (error != null)
            {
                await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }

        public async void OnSyncItems(object sender, EventArgs e)
        {
            await RefreshItems(true, true);
        }

        private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
        {
            using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
            {
                var isExpired = _apiManager.CurrentClient.CurrentUser.MobileServiceAuthenticationToken.IsTokenExpired();
                Debug.WriteLine($"INFORMATION: TODOLIST MobileServiceAuthenticationToken IsExpired: {isExpired.ToString()}");
                todoList.ItemsSource = await _apiManager.GetTodoItemsAsync(syncItems);
            }
        }

        private class ActivityIndicatorScope : IDisposable
        {
            private bool showIndicator;
            private ActivityIndicator indicator;
            private Task indicatorDelay;

            public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
            {
                this.indicator = indicator;
                this.showIndicator = showIndicator;

                if (showIndicator)
                {
                    indicatorDelay = Task.Delay(2000);
                    SetIndicatorActivity(true);
                }
                else
                {
                    indicatorDelay = Task.FromResult(0);
                }
            }

            private void SetIndicatorActivity(bool isActive)
            {
                this.indicator.IsVisible = isActive;
                this.indicator.IsRunning = isActive;
            }

            public void Dispose()
            {
                if (showIndicator)
                {
                    indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }
    }
}

