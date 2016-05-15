using Autofac;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;
using esperancephone.Pages;

namespace esperancephone.Helpers
{
    public static class Services
    {
        public static void ResetServices()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var telecommunicationService = scope.Resolve<ITeleCommunicationService>();
                telecommunicationService.CurrentSession = null;

                var contactsService = scope.Resolve<IContactsService>();
                contactsService.CurrentContact = null;
            }
        }

        public static void SetCurrentPageCache(CurrentPageCacheModel cache)
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var settingsService = scope.Resolve<ISettingsService>();
                settingsService.CurrentPageCacheModel = cache;
            }
        }
    }
}