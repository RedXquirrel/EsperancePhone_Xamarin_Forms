using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Extensions;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.Pages
{
    public partial class PersonasPage : ContentPage
    {
        public PersonasPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
            

            Setup();
        }

        private void Setup()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            try
            {

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                this.BindingContext = scope.Resolve<PaperviewsViewModel>();

                var settingsService = scope.Resolve<ISettingsService>();
                switch (settingsService.UserMode)
                {
                    case UserMode.Basic:
                        Resources["PageStyle"] = EsperancePhoneFormsApplication.Application.Resources["PersonasPageBasicMasterDetailNavigationStyle"];
                        break;
                    case UserMode.Advanced:
                        Resources["PageStyle"] = EsperancePhoneFormsApplication.Application.Resources["PersonasPageStandardMasterDetailNavigationStyle"];
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var navigationService = scope.Resolve<INavigationService>();
                navigationService.CurrentPage = this;

                this.WriteLineInstanceAndInstanceId();

                ((StandardViewModel)this.BindingContext).Navigator = (INavigation)this.Navigation;
            }
            }
            catch (Exception ex)
            {

            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Setup();
        }
    }
}
