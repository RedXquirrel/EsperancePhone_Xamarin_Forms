using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.DataTemplates.ContactListCells;
using esperancephone.DataTemplates.CustomCells;
using esperancephone.Models;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.DataTemplates
{
    public class ContactListDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public ContactListDataTemplateSelector()
        {
            this._phoneCell = new DataTemplate(typeof(PhoneCell));
            this._displayNameCell = new DataTemplate(typeof(DisplayNameCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate response = null;

            var viewModel = item as ContactListItemViewModel;
            if (viewModel == null) return null;

            switch (viewModel.TemplateSelectorType)
            {
                case ContactListItemType.DisplayName:
                    response = _displayNameCell;
                    break;
                case ContactListItemType.Phones:
                    response = _phoneCell;
                    break;
                case ContactListItemType.Email:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return response;
        }

        private readonly DataTemplate _phoneCell;
        private readonly DataTemplate _displayNameCell;
    }
}
