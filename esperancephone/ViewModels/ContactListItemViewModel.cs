using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Helpers;
using esperancephone.Models;

namespace esperancephone.ViewModels
{
    public class ContactListItemViewModel : ViewModelBase
    {
        public ContactListItemType TemplateSelectorType { get; set; }
        public object Data { get; set; }
    }
}