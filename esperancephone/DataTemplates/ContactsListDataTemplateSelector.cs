using esperancephone.DataTemplates.CustomCells;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.DataTemplates
{
    public class ContactsListDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public ContactsListDataTemplateSelector()
        {
            this._isPersonantContactListItemDataTemplate = new DataTemplate(typeof(ContactListItemPersonantCell));
            this._isNotPersonantContactListItemDataTemplate = new DataTemplate(typeof(ContactListItemNonPersonantCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var viewModel = item as ContactsListItemViewModel;
            if (viewModel == null) return null;

            return viewModel.IsPersonant ? this._isPersonantContactListItemDataTemplate : this._isNotPersonantContactListItemDataTemplate;
        }

        private readonly DataTemplate _isPersonantContactListItemDataTemplate;
        private readonly DataTemplate _isNotPersonantContactListItemDataTemplate;
    }
}