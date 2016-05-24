using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Extensions;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.Pages
{
    public partial class MainPhonePage : MasterDetailPage
    {
        public Action<bool> MasterDetailToggleAction { get; set; }

        public MainPhonePage()
        {
            try
            {
                InitializeComponent();

                NavigationPage.SetHasNavigationBar(this, false);
            }
            catch (Exception ex)
            {

            }

            this.MasterDetailToggleAction = isOpen =>
            {
                this.IsPresented = !isOpen;
            };

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var navigationService = scope.Resolve<INavigationService>();
                masterPhonePage.ListView.SelectedItem = null;
                navigationService.MasterDetailAction = this.MasterDetailToggleAction;
                IsPresented = false;

                var settingsService = scope.Resolve<ISettingsService>();
                var currentPageCacheModel = settingsService.CurrentPageCacheModel;

                if (currentPageCacheModel != null && currentPageCacheModel.PageCache != null)
                {
                    MainNavigationPage.Navigation.PushAsync(
                        (Page) Activator.CreateInstance(currentPageCacheModel.PageCache));
                    NavigationPage.SetHasNavigationBar(MainNavigationPage.CurrentPage, false);
                }

            }

            masterPhonePage.ListView.ItemSelected += ItemSelected;

            MainIndexMasterPage.MainNavigationPage = this.MainNavigationPage;

            this.PropertyChanged += MainPhonePage_PropertyChanged;

        }

        private void MainPhonePage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(MainPhonePage.IsPresentedProperty.PropertyName))
            {
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var service = scope.Resolve<INavigationService>();

                    if (service.MasterDetailAction == null)
                    {
                        service.MasterDetailAction = this.MasterDetailToggleAction;
                    }

                    service.MasterDetailIsOpen = IsPresented;
                }
            }
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageIndexItemViewModel;

            if(item == null) return;

            if (item.IsPage)
            {
                ProcessPage(item);
            }
            else
            {
                ProcessAction(item);
            }

        }

        private void ProcessAction(MasterPageIndexItemViewModel item)
        {
            if (item?.Action == null) return;

            item.Action.Invoke();
        }

        private void ProcessPage(MasterPageIndexItemViewModel item)
        {
            if (item?.PageType == null) return;

            // Reset Everything
            esperancephone.Helpers.Services.ResetServices();

            // These are the only pages that are to be automatically navigated to
            // when the app is started.
            if (item.PageType == typeof(FavouritesPage) ||
                item.PageType == typeof(RecentPage) ||
                item.PageType == typeof(ContactsPage) ||
                item.PageType == typeof(DiallerPage) ||
                item.PageType == typeof(PersonasPage)
                )
            {
                Helpers.Services.SetCurrentPageCache(new CurrentPageCacheModel() { PageCache = item.PageType });
                if (item.PageType == typeof (PersonasPage))
                {
                    Helpers.Services.SetCurrentPageCacheBottomBarSelection(BottomBarSelection.Paperviews);
                }
                else if (item.PageType == typeof(DiallerPage))
                {
                    Helpers.Services.SetCurrentPageCacheBottomBarSelection(BottomBarSelection.Dialler);
                }
            }
            else
            {
                Helpers.Services.SetCurrentPageCache(new CurrentPageCacheModel() { BottomBarSelection = BottomBarSelection.None });
            }

            MainNavigationPage.Navigation.PushAsync((Page) Activator.CreateInstance(item.PageType));
            NavigationPage.SetHasNavigationBar(MainNavigationPage.CurrentPage, false);

            masterPhonePage.ListView.SelectedItem = null;
            IsPresented = false;
        }
    }
}


