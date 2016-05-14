using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;
using esperancephone.Pages;
using esperancephone.ViewModels.ContactListItemViewModels;
using esperancephone.ViewModels.Shared;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class ContactViewModel : StandardViewModel
    {
        private IContact _contact;
        public IContact Contact
        {
            get { return _contact; }
            set { _contact = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ContactListItemViewModel> _contactListItems;
        public ObservableCollection<ContactListItemViewModel> ContactListItems
        {
            get { return _contactListItems; }
            set { _contactListItems = value; RaisePropertyChanged(); }
        }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value; RaisePropertyChanged(); }
        }

        private string _contactIconCharacter;
        public string ContactIconCharacter
        {
            get { return _contactIconCharacter; }
            set
            {
                _contactIconCharacter = value;
                RaisePropertyChanged();
            }
        }

        private string _closeIconCharacter;

        public string CloseIconCharacter
        {
            get { return _closeIconCharacter; }
            set
            {
                _closeIconCharacter = value;
                RaisePropertyChanged();
            }
        }

        public ContactViewModel()
        {
            this.Title = "Contact";
            this.CloseIconCharacter = "\uf00d";
            this.ContactIconCharacter = "\uf007";

            this.CloseCommand = new Command(async () =>
            {
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var contactsService = scope.Resolve<IContactsService>();
                    contactsService.CurrentContact = null;

                    var navigationService = scope.Resolve<INavigationService>();
                    await navigationService.CurrentPage.Navigation.PopModalAsync();
                }
            });

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IContactsService>();
                this.Contact = service.CurrentContact;
            }

            BuildDataItems();
        }

        private void BuildDataItems()
        {
             var contactListItems = new ObservableCollection<ContactListItemViewModel>();

            contactListItems.Add(new ContactListItemViewModel()
            {
                TemplateSelectorType = ContactListItemType.DisplayName,
                Data = new DisplayNameViewModel() { DisplayName = this.Contact.DisplayName }
            });

            foreach (var phone in this.Contact.Phones)
            {
                contactListItems.Add(new ContactListItemViewModel()
                {
                    TemplateSelectorType = ContactListItemType.Phones,
                    Data = new PhoneViewModel()
                    {
                        Label = phone.Label,
                        Number = phone.Number,
                        CallIcon = "\uf095",
                        CallCommand = new Command(() =>
                        {
                            Debug.WriteLine($"INFORMATION: Call button tapped for {phone.Number}");
                            using (var scope = AppContainer.Container.BeginLifetimeScope())
                            {
                                var navigationService = scope.Resolve<INavigationService>();
                                var telecommunicationService = scope.Resolve<ITeleCommunicationService>();
                                var dialService = scope.Resolve<IDialService>();
                                telecommunicationService.SetDialService(dialService);
                                var commSession = new CommunicationModel();
                                commSession.CommunicationType = CommunicationType.CallAndPersona;
                                commSession.DisplayName = Contact.DisplayName;
                                commSession.PhoneNumber = phone.Number;
                                telecommunicationService.CurrentSession = commSession;
                                navigationService.CurrentPage.Navigation.PushAsync(new PersonasPage());
                                CloseCommand?.Execute(null);
                                //var success = dialService.Dial(phone.Number);
                            }
                        })
                    }
                });
            }

            this.ContactListItems = contactListItems;
        }
    }
}