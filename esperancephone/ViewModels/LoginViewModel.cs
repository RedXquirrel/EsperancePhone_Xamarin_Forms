﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Localisation;
using esperancephone.Pages;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class LoginViewModel : StandardViewModel
    {
        private string _googleButtonText;

        public string GoogleButtonText
        {
            get { return _googleButtonText; }
            set { _googleButtonText = value; RaisePropertyChanged(); }
        }

        private ICommand _googleLoginProviderCommand;

        public ICommand GoogleLoginProviderCommand
        {
            get { return _googleLoginProviderCommand; }
            set { _googleLoginProviderCommand = value; RaisePropertyChanged(); }
        }

        public LoginViewModel()
        {
            
        }

        public LoginViewModel(ISettingsService settingsService) : base(settingsService)
        {
            this.Title = AppResources.LoginPageTitle;
            this.GoogleButtonText = AppResources.GoogleLoginButtonText;
            this.GoogleLoginProviderCommand = new Command((async() =>
            {
                if (EsperancePhoneFormsApplication.Authenticator != null)
                {
                    this.IsBusy = true;
                    EsperancePhoneFormsApplication.Authenticated =
                        await EsperancePhoneFormsApplication.Authenticator.Authenticate();
                }

                // Set syncItems to true in order to synchronize the data on startup when running in offline mode
                if (EsperancePhoneFormsApplication.Authenticated == true)
                {
                    await this.Navigator.PushAsync(new MainPhonePage(), true);
                    this.IsBusy = false;
                }
                else
                {
                    this.IsBusy = false;
                }
            }));
        }
    }
}
