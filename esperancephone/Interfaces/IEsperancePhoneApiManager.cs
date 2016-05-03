using Microsoft.WindowsAzure.MobileServices;

namespace esperancephone.Interfaces
{
    public interface IEsperancePhoneApiManager
    {
        MobileServiceClient CurrentClient { get; }
    }
}