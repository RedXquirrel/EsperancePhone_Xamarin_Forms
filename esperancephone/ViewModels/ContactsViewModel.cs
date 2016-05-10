using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Autofac;
using esperancephone.DataSources;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;
using esperancephone.Pages;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class ContactsViewModel : StandardViewModel
    {
        private ObservableCollection<IContact> _contacts;
        public ObservableCollection<IContact> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; RaisePropertyChanged(); }
        }

        public ICommand SelectedContactCommand => new Command<ContactsListItemViewModel>(async(item) =>
        {
            Debug.WriteLine($"INFORMATION: Selected Contact Display Name is {item.DisplayName}");

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IContactsService>();
                service.CurrentContact = item.Contact;
                var navigationService = scope.Resolve<INavigationService>();
                await navigationService.CurrentPage.Navigation.PushModalAsync(new ContactPage());
            }
        });

        private IContact _selectedContact;
        public IContact SelectedContact
        {
            get { return _selectedContact; }
            set { _selectedContact = value; RaisePropertyChanged(); }
        }

        private IList<ContactsGroupDataSource> _contactGroups;
        public IList<ContactsGroupDataSource> ContactGroups
        {
            get { return _contactGroups; }
            set { _contactGroups = value; RaisePropertyChanged(); }
        }

        public ContactsViewModel()
        {
            this.Title = "Contacts";

            GetContacts();
        }

        private async void GetContacts()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IContactsService>();
                var contacts = await service.GetContacts();
                _contacts = new ObservableCollection<IContact>(contacts);
            }
            SetContacts();
        }

        private void SetContacts()
        {
            try
            {

            var _letterCache = _contacts.FirstOrDefault().DisplayName.Substring(0, 1).ToUpper();

            List<ContactsGroupDataSource> groups = new List<ContactsGroupDataSource>();
            ContactsGroupDataSource group = new ContactsGroupDataSource(_letterCache, _letterCache, string.Empty);

            foreach (var contact in _contacts)
            {
                if (contact.DisplayName.Substring(0, 1).ToUpper().Equals(_letterCache))
                {
                    group.Add(new ContactsListItemViewModel()
                    {
                        Contact = contact,
                        DisplayName = contact.DisplayName,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        IconKey = "\uf007",
                        IsPersonant = false
                    });
                }
                else
                {
                        groups.Add(group);
                        _letterCache = contact.DisplayName.Substring(0, 1).ToUpper();
                    group = new ContactsGroupDataSource(_letterCache, _letterCache, string.Empty);

                    group.Add(new ContactsListItemViewModel()
                    {
                        Contact = contact,
                        DisplayName = contact.DisplayName,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        IconKey = "\uf007",
                        IsPersonant = false
                    });
                }
            }

                groups.Add(group);



                ContactGroups = groups;
            }
            catch (Exception ex)
            {
            }

        }

    }
}