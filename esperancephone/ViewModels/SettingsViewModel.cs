using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;
using esperancephone.Pages;
using esperancephone.ViewModels.PersonaListItemViewModels;
using esperancephone.ViewModels.SettingListItemViewModels;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class SettingsViewModel : StandardViewModel
    {
        private SettingsListItemViewModel _selectedListItem;
        private bool _isListUpdating;
        private bool _isSettingItemSelected;

        public ICommand SelectedListItemCommand => new Command<SettingsListItemViewModel>((item) =>
        {
            Debug.WriteLine($"INFORMATION: Selected Setting List Data Item class is {item.Data.GetType().ToString()}");

            if (item.Data.GetType() == typeof (SettingViewModel))
            {
                if (_isListUpdating) return;

                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    _selectedListItem = item;
                    if (_selectedListItem != null)
                    {
                        _isSettingItemSelected = item.Data.GetType() == typeof (SettingViewModel);
                    }

                    _isListUpdating = true;

                    _isListUpdating = false;
                }
            }
        });

        private ObservableCollection<SettingsListItemViewModel> _settingsListItems;
        public ObservableCollection<SettingsListItemViewModel> SettingsListItems
        {
            get { return _settingsListItems; }
            set { _settingsListItems = value; RaisePropertyChanged(); }
        }

        public SettingsViewModel()
        {
            this.Title = "Settings";

            BuildDataItems();
        }

        private void BuildDataItems()
        {
            var listItems = new ObservableCollection<SettingsListItemViewModel>();

            listItems.Add(
                new SettingsListItemViewModel()
                {
                    TemplateSelectorType = SettingsListItemType.Switch,
                    Data = GetSettingUserModeViewModel()
                });
            this.SettingsListItems = listItems;
        }

        private SettingUserModeViewModel GetSettingUserModeViewModel()
        {
            return new SettingUserModeViewModel()
            {
                DisplayName = "Advanced User Mode"
            };
        }
    }

}