using System;
using esperancephone.DataTemplates.PersonaListCells;
using esperancephone.Models;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.DataTemplates
{
    public class SettingsListDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public SettingsListDataTemplateSelector()
        {
            this._switchCell = new DataTemplate(typeof(Shared.SwitchCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate response = null;

            var viewModel = item as SettingsListItemViewModel;
            if (viewModel == null) return null;

            switch (viewModel.TemplateSelectorType)
            {
                case SettingsListItemType.Switch:
                    response = this._switchCell;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return response;
        }

        private readonly DataTemplate _switchCell;
    }
}