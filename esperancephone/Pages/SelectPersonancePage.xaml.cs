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
    public partial class SelectPersonancePage : ContentPage
    {
        public SelectPersonancePage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                this.BindingContext = AppContainer.Container.Resolve<SelectPersonanceViewModel>();
            }

            Debug.WriteLine($"INFORMATION: ViewModelType is {this.BindingContext.GetType().Name}");

            ((StandardViewModel)this.BindingContext).Navigator = (INavigation)this.Navigation;

            GetContacts();
        }

        private static async void GetContacts()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var service = AppContainer.Container.Resolve<IContactsService>();
                var contacts = await service.GetContacts();
            }
        }
    }
}
