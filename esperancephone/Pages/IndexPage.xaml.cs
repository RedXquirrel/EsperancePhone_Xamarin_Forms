using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Extensions;
using esperancephone.Ioc;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.Pages
{
    public partial class IndexPage : ContentPage
    {
        public IndexPage()
        {
            InitializeComponent();

            this.WriteLineInstanceAndInstanceId();

            //NavigationPage.SetHasNavigationBar(this, false);

            //using (var scope = AppContainer.Container.BeginLifetimeScope())
            //{
            //    this.BindingContext = AppContainer.Container.Resolve<IndexViewModel>();
            //}

            //Debug.WriteLine($"INFORMATION: ViewModelType is {this.BindingContext.GetType().Name}");

            //((StandardViewModel)this.BindingContext).Navigator = (INavigation)this.Navigation;
        }
    }
}
