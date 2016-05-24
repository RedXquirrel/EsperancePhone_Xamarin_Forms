using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.DataSources;
using esperancephone.Extensions;
using esperancephone.Helpers;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.Pages
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
            Setup();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Setup();
        }

        private void Setup()
        {
            this.SetUpPage<ContactsViewModel>(false, "ContactsPageBasicMasterDetailNavigationStyle", "ContactsPageStandardMasterDetailNavigationStyle");

            //using (var scope = AppContainer.Container.BeginLifetimeScope())
            //{
            //    this.BindingContext = scope.Resolve<ContactsViewModel>();

            //    this.WriteLineInstanceAndInstanceId();

            //    ((StandardViewModel) this.BindingContext).Navigator = (INavigation) this.Navigation;

            //    var navigationService = scope.Resolve<INavigationService>();
            //    navigationService.CurrentPage = this;
            //}
        }
    }
}
