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
