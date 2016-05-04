﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Ioc;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.Pages
{
    public partial class PersonancePage : ContentPage
    {
        public PersonancePage()
        {
            InitializeComponent();

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                this.BindingContext = AppContainer.Container.Resolve<PersonanceViewModel>();
            }

            Debug.WriteLine($"INFORMATION: ViewModelType is {this.BindingContext.GetType().Name}");

            ((StandardViewModel)this.BindingContext).Navigator = (INavigation)this.Navigation;
        }
    }
}