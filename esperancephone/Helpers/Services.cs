using System;
using Autofac;
using esperancephone.Extensions;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;
using esperancephone.Pages;
using esperancephone.ViewModels;
using Xamarin.Forms;

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

                //var paperviewService = scope.Resolve<IPaperviewService>();
                //paperviewService.CurrentPaperview = null;
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

        public static void SetUpPage<T>(this ContentPage page, bool hasNavigationBar, string basicMasterDetailNavigationStyle, string standardMasterDetailNavigationStyle)
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                NavigationPage.SetHasNavigationBar(page, hasNavigationBar);
                page.BindingContext = scope.Resolve<T>();
                if (page.Resources != null)
                {
                    var settingsService = scope.Resolve<ISettingsService>();
                    switch (settingsService.UserMode)
                    {
                        case UserMode.Basic:
                            page.Resources["PageStyle"] =
                                EsperancePhoneFormsApplication.Application.Resources[
                                    basicMasterDetailNavigationStyle];
                            break;
                        case UserMode.Advanced:
                            page.Resources["PageStyle"] =
                                EsperancePhoneFormsApplication.Application.Resources[
                                    standardMasterDetailNavigationStyle];
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                var navigationService = scope.Resolve<INavigationService>();
                navigationService.CurrentPage = page;

                page.WriteLineInstanceAndInstanceId();

                ((StandardViewModel) page.BindingContext).Navigator = (INavigation) page.Navigation;
            }
        }

        public static void SetCurrentPageCacheBottomBarSelection(BottomBarSelection selection)
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var settingsService = scope.Resolve<ISettingsService>();
                if(settingsService.CurrentPageCacheModel == null) settingsService.CurrentPageCacheModel = new CurrentPageCacheModel();
                settingsService.CurrentPageCacheModel.BottomBarSelection = selection;
            }
        }

        public static void SetCurrentPageCacheBottomBarSelection(this object viewModel)
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var response = BottomBarSelection.None;

                var settingsService = scope.Resolve<ISettingsService>();
                if (settingsService.CurrentPageCacheModel == null) settingsService.CurrentPageCacheModel = new CurrentPageCacheModel();

                if (viewModel.GetType().Name.Equals(typeof(FavouritesViewModel).Name))
                {
                    response = BottomBarSelection.Contacts;    
                }
                else if (viewModel.GetType().Name.Equals(typeof(RecentViewModel).Name))
                {
                    response = BottomBarSelection.Recent;
                }
                else if (viewModel.GetType().Name.Equals(typeof(ContactsViewModel).Name))
                {
                    response = BottomBarSelection.Contacts;
                }
                else if (viewModel.GetType().Name.Equals(typeof(DiallerViewModel).Name))
                {
                    response = BottomBarSelection.Dialler;
                }
                else if (viewModel.GetType().Name.Equals(typeof(PaperviewsViewModel).Name))
                {
                    response = BottomBarSelection.Paperviews;
                }

                settingsService.CurrentPageCacheModel.BottomBarSelection = response;
            }
        }
    }
}