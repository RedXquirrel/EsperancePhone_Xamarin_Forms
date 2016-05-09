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
    public partial class AboutCaptainXamtasticPage : ContentPage
    {
        public AboutCaptainXamtasticPage()
        {
            InitializeComponent();

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                this.BindingContext = AppContainer.Container.Resolve<AboutCaptainXamtasticViewModel>();
            }

            this.WriteLineInstanceAndInstanceId();

            ((StandardViewModel)this.BindingContext).Navigator = (INavigation)this.Navigation;
        }
    }
}
