using esperancephone.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Helpers;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Pages;
using Microsoft.WindowsAzure.MobileServices;

namespace esperancephone.DataSources
{
    public class MasterDetailItemGroupDataSource : List<MasterPageIndexItemViewModel>
    {
        private const string ExhibitsViewModelKey = "ExhibitsViewModel";
        private const string Error404ViewModelKey = "Error404ViewModel";
        private const string AddPublishersViewModelKey = "AddPublisherViewModel";
        private const string EditDocumentViewModelKey = "EditDocumentViewModel";

        public string Title { get; set; }
        public string ShortName { get; set; } // used for jump lists, must be 1 character
        public string Subtitle { get; set; }
        private MasterDetailItemGroupDataSource(string title, string shortName, string subtitle)
        {
            Title = title;
            ShortName = shortName;
            Subtitle = subtitle;
        }

        public static IList<MasterDetailItemGroupDataSource> Groups { private set; get; }

        static MasterDetailItemGroupDataSource()
        {
            List<MasterDetailItemGroupDataSource> Groups = new List<MasterDetailItemGroupDataSource>
            {
                new MasterDetailItemGroupDataSource("Esperance", string.Empty, "To reach out with especial personance")
                {
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf005", Title = "About", PageType = typeof(AboutCaptainXamtasticPage) },
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf005", Title = "FAQ", PageType = typeof(FaqPage) },
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf005", Title = "Terms and Conditions", PageType = typeof(FaqPage) },
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf013", Title = "Settings", PageType = typeof(SettingsPage) },
                    new MasterPageIndexItemViewModel { IsPage = false, IconKey = "\uf08b", Title = "Logout", Action =
                        async() =>
                        {
                            using (var scope = AppContainer.Container.BeginLifetimeScope())
                            {
                                var apiManager = AppContainer.Container.Resolve<IEsperancePhoneApiManager>();

                                await apiManager.CurrentClient.LogoutAsync();

                                var navigationService = AppContainer.Container.Resolve<INavigationService>();
                                await navigationService.RootNavigation.PushAsync(new LoginPage());

                                var settingsService = AppContainer.Container.Resolve<ISettingsService>();
                                settingsService.UserId = null;
                                settingsService.MobileServiceAuthenticationToken = null;

                                Debug.WriteLine($"INFORMATION: Logged out at {DateTime.UtcNow} UTC");
                            }
                        } },
                },
                new MasterDetailItemGroupDataSource("Paperview", string.Empty, "Our universal distribution format")
                {
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf005", Title = "About", PageType = typeof(AboutCaptainXamtasticPage) },
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf005", Title = "FAQ", PageType = typeof(FaqPage) },
                },
                new MasterDetailItemGroupDataSource("Corporate", string.Empty, "Say hello to Captain Xamtastic")
                {
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf005", Title = "About", PageType = typeof(AboutCaptainXamtasticPage) },
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf095", Title = "Contact Us", PageType = typeof(ContactUsPage) },
                },
                new MasterDetailItemGroupDataSource("Feedback", string.Empty, "Help us improve our app")
                {
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf24a", Title = "Be Anonymous", PageType = typeof(TodoList) },
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf24a", Title = "Be You", PageType = typeof(TodoList) },
                },
            };

            MasterDetailItemGroupDataSource.Groups = Groups;
        }

    }
}
