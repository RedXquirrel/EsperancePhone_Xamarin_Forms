using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Autofac;
using esperancephone.DataSources;
using esperancephone.Helpers;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;
using esperancephone.Pages;
using esperancephone.ViewModels.Shared;
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
            if(item.Data != null) return; // Is not an actual Contact

            this.IsBusy = true;
            Debug.WriteLine($"INFORMATION: Selected Contact Display Name is {item.DisplayName}");

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IContactsService>();
                service.CurrentContact = item.Contact;
                var navigationService = scope.Resolve<INavigationService>();
                await navigationService.CurrentPage.Navigation.PushModalAsync(new ContactPage());
                this.IsBusy = false;
            }
        });

        private IContact _selectedContact;
        public IContact SelectedContact
        {
            get { return _selectedContact; }
            set { _selectedContact = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ContactsGroupDataSource> _contactGroups;
        public ObservableCollection<ContactsGroupDataSource> ContactGroups
        {
            get { return _contactGroups; }
            set { _contactGroups = value; RaisePropertyChanged(); }
        }

        public ContactsViewModel()
        {
            this.Title = "Contacts";
            this.SetCurrentPageCacheBottomBarSelection();
            GetContacts();
        }

        private async void GetContacts()
        {
            this.IsBusy = true;
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

                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var service = scope.Resolve<IPaperviewService>();

                    if (service.CurrentPaperview != null)
                    {
                        ContactsGroupDataSource selectedPaperviewGroup =
                            new ContactsGroupDataSource("Selected Paperview", string.Empty,
                                "To send this, select a Contact or Dial");
                        selectedPaperviewGroup.Add(new ContactsListItemViewModel()
                        {
                            ListItemType = ContactsListItemItemTemplates.Paperview,
                            Data = new LabelAndCommandTextViewModel() { IsSubItem = true, LabelText = service.CurrentPaperview.DisplayName, CommandText = "remove", Command = new Command(
                                () =>
                                {
                                    this.ContactGroups.RemoveAt(0);
                                    service.CurrentPaperview = null;
                                    this.PersonanceCommand.Execute(null);
                                })}
                        });
                        groups.Add(selectedPaperviewGroup);
                    }

                    ContactsGroupDataSource diallerGroup =
                        new ContactsGroupDataSource("Direct Dial", string.Empty,
                            "Dial the number directly into the keypad");
                    diallerGroup.Add(new ContactsListItemViewModel()
                    {
                        ListItemType = ContactsListItemItemTemplates.Dialler,
                        Data = new LabelAndCommandIconViewModel()
                        {
                            IsSubItem = true,
                            LabelText = "Dialler",
                            IconCharacter = "\uf098",
                            Command = new Command(
                            () =>
                            {
                                DiallerCommand.Execute(null);
                            })
                        }
                    });
                    groups.Add(diallerGroup);


                }

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
                        ListItemType = ContactsListItemItemTemplates.NonPersonant
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
                        ListItemType = ContactsListItemItemTemplates.NonPersonant
                    });
                }
            }

                groups.Add(group);



                ContactGroups = new ObservableCollection<ContactsGroupDataSource>(groups);
            }
            catch (Exception ex)
            {
            }
            this.IsBusy = false;
        }

    }
}