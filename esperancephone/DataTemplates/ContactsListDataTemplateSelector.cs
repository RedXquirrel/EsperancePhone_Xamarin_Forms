using System;
using esperancephone.DataTemplates.CustomCells;
using esperancephone.DataTemplates.PersonaListCells;
using esperancephone.DataTemplates.Shared;
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
            this._paperviewContactListItemDataTemplate = new DataTemplate(typeof(LabelAndCommandTextCell));
            this._diallerListItemDataTemplate = new DataTemplate(typeof(LabelAndCommandIconCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var viewModel = item as ContactsListItemViewModel;
            if (viewModel == null) return null;

            DataTemplate response = null;

            switch (viewModel.ListItemType)
            {
                case ContactsListItemItemTemplates.Personant:
                    response = _isPersonantContactListItemDataTemplate;
                    break;
                case ContactsListItemItemTemplates.NonPersonant:
                    response = _isNotPersonantContactListItemDataTemplate;
                    break;
                case ContactsListItemItemTemplates.Paperview:
                    response = _paperviewContactListItemDataTemplate;
                    break;
                case ContactsListItemItemTemplates.Dialler:
                    response = _diallerListItemDataTemplate;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return response;
        }

        private readonly DataTemplate _isPersonantContactListItemDataTemplate;
        private readonly DataTemplate _isNotPersonantContactListItemDataTemplate;
        private readonly DataTemplate _paperviewContactListItemDataTemplate;
        private readonly DataTemplate _diallerListItemDataTemplate;
    }
}