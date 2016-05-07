using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.Pages
{
    public partial class DiallerPage : ContentPage
    {
        public DiallerPage()
        {
            InitializeComponent();

            this.DiallerView.HostPage = this; // so that the DiallerView can call ActionSheets directly.

            NavigationPage.SetHasNavigationBar(this, false);

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                this.BindingContext = AppContainer.Container.Resolve<DiallerViewModel>();
                var navigationService = AppContainer.Container.Resolve<INavigationService>();
                navigationService.CurrentPage = this;
            }

            Debug.WriteLine($"INFORMATION: ViewModelType is {this.BindingContext.GetType().Name}");

            ((StandardViewModel)this.BindingContext).Navigator = (INavigation)this.Navigation;
        }
    }
}
