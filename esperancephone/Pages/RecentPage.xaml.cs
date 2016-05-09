using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Extensions;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.Pages
{
    public partial class RecentPage : ContentPage
    {
        public RecentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                this.BindingContext = scope.Resolve<RecentViewModel>();
                var navigationService = scope.Resolve<INavigationService>();
                navigationService.CurrentPage = this;

                this.WriteLineInstanceAndInstanceId();

                ((StandardViewModel)this.BindingContext).Navigator = (INavigation)this.Navigation;
            }
        }
    }
}
