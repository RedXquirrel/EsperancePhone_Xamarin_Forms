using esperancephone.Helpers;
using esperancephone.Models;

namespace esperancephone.ViewModels
{
    public class ContactListItemViewModel
    {
        public ContactListItemType TemplateSelectorType { get; set; }
        public object Data { get; set; }
    }
}