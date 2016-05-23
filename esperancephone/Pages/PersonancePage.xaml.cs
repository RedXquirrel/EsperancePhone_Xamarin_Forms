using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class PersonancePage : ContentPage
    {
        public PersonancePage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this,false);

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                this.BindingContext = scope.Resolve<PersonanceViewModel>();

                var settingsService = scope.Resolve<ISettingsService>();

                switch (settingsService.UserMode)
                {
                    case UserMode.Basic:
                        Resources["PageStyle"] = Resources["PersonasPageBasicMasterDetailNavigationStyle"];
                        break;
                    case UserMode.Advanced:
                        Resources["PageStyle"] = Resources["PersonasPageStandardMasterDetailNavigationStyle"];
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                this.WriteLineInstanceAndInstanceId();

                ((StandardViewModel) this.BindingContext).Navigator = (INavigation) this.Navigation;
            }
        }
    }
}
