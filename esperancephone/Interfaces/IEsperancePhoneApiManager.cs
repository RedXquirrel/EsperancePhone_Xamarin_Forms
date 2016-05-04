using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace esperancephone.Interfaces
{
    public interface IEsperancePhoneApiManager
    {
        MobileServiceClient CurrentClient { get; }
        bool IsOfflineEnabled { get; }

        Task<ObservableCollection<TodoItem>> GetTodoItemsAsync(bool syncItems = false);
        Task SaveTaskAsync(TodoItem item);


    }
}