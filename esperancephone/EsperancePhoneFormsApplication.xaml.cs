using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Helpers;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Pages;
using Xamarin.Forms;

namespace esperancephone
{
    public partial class EsperancePhoneFormsApplication : Application
    {
        public static EsperancePhoneFormsApplication Application { get; set; }

        public static IAuthenticate Authenticator { get; private set; }
        public static bool Authenticated = false;

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        public EsperancePhoneFormsApplication()
        {
            InitializeComponent();

            Application = this;

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var service = AppContainer.Container.Resolve <ISettingsService>();
                service.ApplicationName = "Esperance Phone";
            }

                Debug.WriteLine($"INFORMATION (EsperancePhoneFormsApplication): UserId = {Settings.UserId}");

            Authenticated = !string.IsNullOrEmpty(Settings.UserId);

            if (Authenticated)
            {
                MainPage = new NavigationPage(new MainPhonePage());

                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var service = AppContainer.Container.Resolve<INavigationService>();
                    service.RootNavigation = MainPage.Navigation;
                }
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
