using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Extensions;
using esperancephone.Helpers;
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

            Setup();
        }

        private void Setup()
        {
            this.SetUpPage<DiallerViewModel>(false, "DiallerPageBasicMasterDetailNavigationStyle", "DiallerPageStandardMasterDetailNavigationStyle");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Setup();
        }
    }
}
