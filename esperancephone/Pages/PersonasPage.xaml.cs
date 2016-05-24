using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Extensions;
using esperancephone.Helpers;
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
            this.SetUpPage<PaperviewsViewModel>(false, "PersonasPageBasicMasterDetailNavigationStyle", "PersonasPageStandardMasterDetailNavigationStyle");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Setup();
        }
    }
}
