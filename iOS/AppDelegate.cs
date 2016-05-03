using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Foundation;
using UIKit;

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Helpers;
using esperancephone.Interfaces;
using esperancephone.Ioc;

namespace esperancephone.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IAuthenticate
    {
        // Define a authenticated user.
        private MobileServiceUser user;

        public async Task<bool> Authenticate()
        {
            var success = false;
            try
            {
                Debug.WriteLine($"INFORMATION (AppDelegate): UserId = {Settings.UserId}");
                // Sign in with Facebook login using a server-managed flow.
                if (user == null)
                {
                    using (var scope = AppContainer.Container.BeginLifetimeScope())
                    {
                        var apiManager = AppContainer.Container.Resolve<IEsperancePhoneApiManager>();
                        user = await apiManager.CurrentClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, MobileServiceAuthenticationProvider.Google);
                    }

                    //user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController,MobileServiceAuthenticationProvider.Google);

                    if (user != null)
                    {
                        UIAlertView avAlert = new UIAlertView("Authentication", "You are now logged in " + user.UserId, null, "OK", null);
                        avAlert.Show();

                        Settings.UserId = user.UserId;
                        
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                UIAlertView avAlert = new UIAlertView("Authentication failed", ex.Message, null, "OK", null);
                avAlert.Show();
            }
            return success;
        }
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            // IMPORTANT: uncomment this code to enable sync on Xamarin.iOS
            // For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
            //SQLitePCL.CurrentPlatform.Init();
            Debug.WriteLine($"INFORMATION (AppDelegate1): UserId = {Settings.UserId}");
            EsperancePhoneFormsApplication.Init(this);
            LoadApplication (new EsperancePhoneFormsApplication(new AppSetup()));

			return base.FinishedLaunching (app, options);
		}
	}
}

