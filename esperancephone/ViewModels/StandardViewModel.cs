using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class StandardViewModel : ViewModelBase
    {
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

        #region Bottom Bar (favourtites, recent, contacts, dialler, personnance

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

        private string _personanceLabelText = "Personance";
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
                
            });

            this.RecentCommand = new Command(() =>
            {
                
            });

            this.ContactsCommand = new Command(() =>
            {
                
            });

            this.DiallerCommand = new Command(() =>
            {
                
            });

            this.PersonanceCommand = new Command(() =>
            {
                
            });

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                _settingsService = scope.Resolve<ISettingsService>();
            }
        }

        public StandardViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

    }
}
