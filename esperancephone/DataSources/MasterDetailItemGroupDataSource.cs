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
                new MasterDetailItemGroupDataSource("Phone", "1", "Make your calls especially personant")
                {
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf098", Title = "Dialler", PageType = typeof(DiallerPage) },
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf06e", Title = "Personance", PageType = typeof(PersonancePage) },
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf0c0", Title = "Contacts", PageType = typeof(ContactsPage) },
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
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf005", Title = "FAQ", PageType = typeof(FaqPage) },
                },
                new MasterDetailItemGroupDataSource("Utilities", "2", "Manage yourself")
                {
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf24a", Title = "To Do List", PageType = typeof(TodoList) },
                },
                new MasterDetailItemGroupDataSource("Corporate", "3", "Say hello to Captain Xamtastic")
                {
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf095", Title = "Contact Us", PageType = typeof(ContactUsPage) },
                    new MasterPageIndexItemViewModel { IsPage = true, IconKey = "\uf005", Title = "About", PageType = typeof(AboutCaptainXamtasticPage) },
                },
            };

            MasterDetailItemGroupDataSource.Groups = Groups;
        }

    }
}
