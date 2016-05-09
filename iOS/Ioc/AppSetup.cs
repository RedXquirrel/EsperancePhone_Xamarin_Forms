using Autofac;
using esperancephone.iOS.Services;
using esperancephone.Interfaces;
using esperancephone.Managers;
using esperancephone.Pages;
using esperancephone.Services;
using esperancephone.ViewModels;

namespace esperancephone.iOS.Ioc
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(new EsperancePhoneApiManager()).As<IEsperancePhoneApiManager>();
            containerBuilder.RegisterInstance(new SettingsService()).As<ISettingsService>();
            containerBuilder.RegisterInstance(new NavigationService()).As<INavigationService>();
            containerBuilder.RegisterInstance(new DiallerService()).SingleInstance().As<IDiallerService>();
            containerBuilder.RegisterInstance(new ContactService()).SingleInstance().As<IContactsService>();
            containerBuilder.RegisterType<LoginViewModel>().SingleInstance();
            containerBuilder.RegisterType<DiallerViewModel>().SingleInstance();
            containerBuilder.RegisterType<PersonanceViewModel>().SingleInstance();
            containerBuilder.RegisterType<ContactsViewModel>().SingleInstance();
            containerBuilder.RegisterType<ContactViewModel>().SingleInstance();
            containerBuilder.RegisterType<ToDoListViewModel>().SingleInstance();
            containerBuilder.RegisterType<SelectPersonanceViewModel>().SingleInstance();
            containerBuilder.RegisterType<FaqViewModel>().SingleInstance();
            containerBuilder.RegisterType<ContactUsViewModel>().SingleInstance();
            containerBuilder.RegisterType<AboutCaptainXamtasticViewModel>().SingleInstance();
            containerBuilder.RegisterType<IndexViewModel>().SingleInstance();
            containerBuilder.RegisterType<MainPhonePage>().SingleInstance();
        }
    }
}