using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Helpers;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;
using esperancephone.Pages;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class StandardViewModel : ViewModelBase
    {
        public string ViewModelInstanceId { get; set; }

        private ISettingsService _settingsService;

        private INavigation _navigator;

        public INavigation Navigator
        {
            get { return _navigator; }
            internal set
            {
                _navigator = value;

                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var service = scope.Resolve<INavigationService>();

                    service.Navigation = _navigator;
                }
            }
        }

        public string ApplicationName => _settingsService.ApplicationName;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }

        private ICommand _masterDetailCommand;

        public ICommand MasterDetailCommand
        {
            get { return _masterDetailCommand; }
            set { _masterDetailCommand = value; RaisePropertyChanged(); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(); }
        }

        #region Bottom Bar (favourtites, recent, contacts, dialler, personnance

        private BottomBarSelection _bottomBarSelection;
        public BottomBarSelection BottomBarSelection
        {
            get { return _bottomBarSelection; }
            set { _bottomBarSelection = value; RaisePropertyChanged(); }
        }

        private string _favouritesIcon;
        public string FavouritesIcon
        {
            get { return _favouritesIcon = "\uf006"; }
            set { _favouritesIcon = value; RaisePropertyChanged(); }
        }

        private string _recentIcon = "\uf017";
        public string RecentIcon
        {
            get { return _recentIcon; }
            set { _recentIcon = value; RaisePropertyChanged(); }
        }

        private string _contactsIcon = "\uf0c0";
        public string ContactsIcon
        {
            get { return _contactsIcon; }
            set { _contactsIcon = value; RaisePropertyChanged(); }
        }

        private string _diallerIcon = "\uf098";
        public string DiallerIcon
        {
            get { return _diallerIcon; }
            set { _diallerIcon = value; RaisePropertyChanged(); }
        }

        private string _personanceIcon = "\uf06e";
        public string PersonanceIcon
        {
            get { return _personanceIcon; }
            set { _personanceIcon = value; RaisePropertyChanged(); }
        }

        private string _favouritesLabelText = "Favourites";
        public string FavouritesLabelText
        {
            get { return _favouritesLabelText; }
            set { _favouritesLabelText = value; RaisePropertyChanged(); }
        }

        private string _recentLabelText = "Recent";
        public string RecentLabelText
        {
            get { return _recentLabelText; }
            set { _recentLabelText = value; RaisePropertyChanged(); }
        }

        private string _contactsLabelText = "Contacts";
        public string ContactsLabelText
        {
            get { return _contactsLabelText; }
            set { _contactsLabelText = value; RaisePropertyChanged(); }
        }

        private string _diallerLabelText = "Dialler";
        public string DiallerLabelText
        {
            get { return _diallerLabelText; }
            set { _diallerLabelText = value; RaisePropertyChanged(); }
        }

        private string _personanceLabelText = "Paperviews";
        public string PersonanceLabelText
        {
            get { return _personanceLabelText; }
            set { _personanceLabelText = value; RaisePropertyChanged(); }
        }

        private ICommand _favouritesCommand;
        public ICommand FavouritesCommand
        {
            get { return _favouritesCommand; }
            set { _favouritesCommand = value; RaisePropertyChanged(); }
        }

        private ICommand _recentCommand;
        public ICommand RecentCommand
        {
            get { return _recentCommand; }
            set { _recentCommand = value; RaisePropertyChanged(); }
        }

        private ICommand _contactsCommand;
        public ICommand ContactsCommand
        {
            get { return _contactsCommand; }
            set { _contactsCommand = value; RaisePropertyChanged(); }
        }

        private ICommand _diallerCommand;
        public ICommand DiallerCommand
        {
            get { return _diallerCommand; }
            set { _diallerCommand = value; RaisePropertyChanged(); }
        }

        private ICommand _personanceCommand;
        public ICommand PersonanceCommand
        {
            get { return _personanceCommand; }
            set { _personanceCommand = value; RaisePropertyChanged(); }
        }

        #endregion

        public StandardViewModel()
        {
            this.ViewModelInstanceId = Guid.NewGuid().ToString();
            Debug.WriteLine($"INFORMATION: {this.GetType().Name} Instance ID is {this.ViewModelInstanceId}");

            this.MasterDetailCommand = new Command((p) =>
            {
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var service = scope.Resolve<INavigationService>();

                    service.MasterDetailAction.Invoke(service.MasterDetailIsOpen);
                }
            });

            this.FavouritesCommand = new Command(() =>
            {
                Helpers.Services.ResetServices();
                Helpers.Services.SetCurrentPageCache(new CurrentPageCacheModel() { PageCache = typeof(FavouritesPage), ViewModelCache = typeof(FavouritesViewModel), BottomBarSelection = BottomBarSelection.Favourites });
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var settingsService = scope.Resolve<ISettingsService>();

                    if (this.GetType() != settingsService.CurrentPageCacheModel.ViewModelCache)
                    {
                        var navigationService = scope.Resolve<INavigationService>();
                        navigationService.Navigation.PushAsync(new FavouritesPage(), false);
                    }
                }
            });

            this.RecentCommand = new Command(() =>
            {
                Helpers.Services.ResetServices();
                Helpers.Services.SetCurrentPageCache(new CurrentPageCacheModel() { PageCache = typeof(RecentPage), ViewModelCache = typeof(RecentViewModel), BottomBarSelection = BottomBarSelection.Recent });
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var settingsService = scope.Resolve<ISettingsService>();

                    if (this.GetType() != settingsService.CurrentPageCacheModel.ViewModelCache)
                    {
                        var navigationService = scope.Resolve<INavigationService>();
                        navigationService.Navigation.PushAsync(new RecentPage(), false);
                    }
                }
            });

            this.ContactsCommand = new Command(() =>
            {
                Helpers.Services.ResetServices();
                Helpers.Services.SetCurrentPageCache(new CurrentPageCacheModel() { PageCache = typeof(ContactsPage), ViewModelCache = typeof(ContactsViewModel), BottomBarSelection = BottomBarSelection.Contacts });
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var settingsService = scope.Resolve<ISettingsService>();

                    if (this.GetType() != settingsService.CurrentPageCacheModel.ViewModelCache)
                    {
                        var navigationService = scope.Resolve<INavigationService>();
                        navigationService.Navigation.PushAsync(new ContactsPage(), false);
                    }
                }
            });

            this.DiallerCommand = new Command(() =>
            {
                Helpers.Services.ResetServices();
                Helpers.Services.SetCurrentPageCache(new CurrentPageCacheModel() { PageCache = typeof(DiallerPage), ViewModelCache = typeof(DiallerViewModel), BottomBarSelection = BottomBarSelection.Dialler});
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var settingsService = scope.Resolve<ISettingsService>();

                    if (this.GetType() != settingsService.CurrentPageCacheModel.ViewModelCache)
                    {
                        var navigationService = scope.Resolve<INavigationService>();
                        navigationService.Navigation.PushAsync(new DiallerPage(), false);
                    }
                }
            });

            this.PersonanceCommand = new Command(() =>
            {
                Helpers.Services.ResetServices();
                Helpers.Services.SetCurrentPageCache(new CurrentPageCacheModel() { PageCache = typeof(PersonasPage), ViewModelCache = typeof(PaperviewsViewModel), BottomBarSelection = BottomBarSelection.Paperviews });
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var settingsService = scope.Resolve<ISettingsService>();

                    if (this.GetType() != settingsService.CurrentPageCacheModel.ViewModelCache)
                    {
                        var navigationService = scope.Resolve<INavigationService>();
                        navigationService.Navigation.PushAsync(new PersonasPage(), false);
                    }
                }
            });

            this.SetCurrentPageCacheBottomBarSelection();

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                _settingsService = scope.Resolve<ISettingsService>();
                var currentPageCacheModel = _settingsService.CurrentPageCacheModel;
                if(currentPageCacheModel != null) this.BottomBarSelection = currentPageCacheModel.BottomBarSelection;
            }
        }

        public StandardViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

    }
}
