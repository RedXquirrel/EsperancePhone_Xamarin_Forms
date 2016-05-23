using System;
using Autofac;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;

namespace esperancephone.ViewModels.SettingListItemViewModels
{
    public class SettingUserModeViewModel : StandardViewModel
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; RaisePropertyChanged(); }
        }

        public UserMode UserMode
        {
            get { return GetUserMode(); }
            set { SetUserMode(value); RaisePropertyChanged(); }
        }

        public bool IsSubItem { get; set; } = false;

        private static void SetUserMode(UserMode value)
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var settingsService = scope.Resolve<ISettingsService>();

                settingsService.UserMode = value;
            }
        }

        private static UserMode GetUserMode()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var settingsService = scope.Resolve<ISettingsService>();

                return settingsService.UserMode;
            }
        }
    }
}