using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Models;

namespace esperancephone.ViewModels
{
    public class SettingsListItemViewModel : ViewModelBase
    {
        public SettingsListItemType TemplateSelectorType { get; set; }
        public object Data { get; set; }
    }
}