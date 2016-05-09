using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Extensions;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.Pages
{
    public partial class MainPhonePage : MasterDetailPage
    {
        public Action<bool> MasterDetailToggleAction { get; set; }

        public MainPhonePage()
        {
            try
            {
                InitializeComponent();

                NavigationPage.SetHasNavigationBar(this, false);
            }
            catch (Exception ex)
            {

            }

            this.MasterDetailToggleAction = isOpen =>
            {
                this.IsPresented = !isOpen;
            };

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<INavigationService>();
                masterPhonePage.ListView.SelectedItem = null;
                service.MasterDetailAction = this.MasterDetailToggleAction;
                IsPresented = false;
            }

            masterPhonePage.ListView.ItemSelected += ItemSelected;

            MainIndexMasterPage.MainNavigationPage = this.MainNavigationPage;

            this.PropertyChanged += MainPhonePage_PropertyChanged;

        }

        private void MainPhonePage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(MainPhonePage.IsPresentedProperty.PropertyName))
            {
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var service = scope.Resolve<INavigationService>();

                    if (service.MasterDetailAction == null)
                    {
                        service.MasterDetailAction = this.MasterDetailToggleAction;
                    }

                    service.MasterDetailIsOpen = IsPresented;
                }
            }
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageIndexItemViewModel;

            if(item == null) return;

            if (item.IsPage)
            {
                ProcessPage(item);
            }
            else
            {
                ProcessAction(item);
            }

        }

        private void ProcessAction(MasterPageIndexItemViewModel item)
        {
            if (item?.Action == null) return;

            item.Action.Invoke();
        }

        private void ProcessPage(MasterPageIndexItemViewModel item)
        {
            if (item?.PageType == null) return;

            MainNavigationPage.Navigation.PushAsync((Page) Activator.CreateInstance(item.PageType));
            NavigationPage.SetHasNavigationBar(MainNavigationPage.CurrentPage, false);

            masterPhonePage.ListView.SelectedItem = null;
            IsPresented = false;
        }
    }
}


