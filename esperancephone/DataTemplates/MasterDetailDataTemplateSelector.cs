using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.DataTemplates.CustomCells;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.DataTemplates
{
    public class MasterDetailDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public MasterDetailDataTemplateSelector()
        {
            this._pageDataTemplate = new DataTemplate(typeof(PageCell));
            this._functionDataTemplate = new DataTemplate(typeof(FunctionCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var viewModel = item as MasterPageIndexItemViewModel;
            if (viewModel == null) return null;

            return viewModel.IsPage ? this._pageDataTemplate : this._functionDataTemplate;
        }

        private readonly DataTemplate _pageDataTemplate;
        private readonly DataTemplate _functionDataTemplate;
    }
}
