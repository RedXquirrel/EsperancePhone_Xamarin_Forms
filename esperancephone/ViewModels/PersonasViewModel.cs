using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;
using esperancephone.Pages;
using esperancephone.ViewModels.PersonaListItemViewModels;
using esperancephone.ViewModels.Shared;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class PersonasViewModel : StandardViewModel
    {
        private bool _contactSelected;
        private bool _personaSelected;

        private PersonaListItemViewModel _selectedListItem;

        public ICommand SelectedPersonaListItemCommand => new Command<PersonaListItemViewModel>((item) =>
        {
            Debug.WriteLine($"INFORMATION: Selected Persona List Data Item class is {item.Data.GetType().ToString()}");

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                SetSelectedItem(item);
            }
        });

        private void SetSelectedItem(PersonaListItemViewModel item)
        {
            _selectedListItem = item;
            if (_selectedListItem != null)
            {
                _personaSelected = item.Data.GetType() == typeof (PersonaViewModel);
            }
        }

        private ObservableCollection<PersonaListItemViewModel> _personaListItems;
        public ObservableCollection<PersonaListItemViewModel> PersonaListItems
        {
            get { return _personaListItems; }
            set { _personaListItems = value; RaisePropertyChanged(); }
        }

        public PersonasViewModel()
        {
            this.Title = "Personas";

            BuildDataItems();
        }

        private void BuildDataItems()
        {
            var listItems = new ObservableCollection<PersonaListItemViewModel>();

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var telecommunicationService = scope.Resolve<ITeleCommunicationService>();
                //var dialService = scope.Resolve<IDialService>();
                //telecommunicationService.SetDialService(dialService);

                _contactSelected = telecommunicationService.CurrentSession != null ? true : false;

                if (_contactSelected)
                {
                    listItems.Add(new PersonaListItemViewModel()
                    {
                        TemplateSelectorType = PersonaListItemType.DisplayName,
                        Data = new DisplayNameViewModel() { DisplayName = telecommunicationService.CurrentSession.DisplayName }
                    });

                    listItems.Add(new PersonaListItemViewModel()
                    {
                        TemplateSelectorType = PersonaListItemType.PhoneNumber,
                        Data = new PhoneNumberViewModel() { PhoneNumber = telecommunicationService.CurrentSession.PhoneNumber }
                    });
                }

                listItems.Add(new PersonaListItemViewModel()
                {
                    TemplateSelectorType = PersonaListItemType.Persona,
                    Data = new PersonaViewModel() { Title = "xPersonal Item 0" }
                });
                listItems.Add(new PersonaListItemViewModel()
                {
                    TemplateSelectorType = PersonaListItemType.Persona,
                    Data = new PersonaViewModel() { Title = "xPersonal Item 1" }
                });
                listItems.Add(new PersonaListItemViewModel()
                {
                    TemplateSelectorType = PersonaListItemType.Persona,
                    Data = new PersonaViewModel() { Title = "xPersonal Item 2" }
                });
                listItems.Add(new PersonaListItemViewModel()
                {
                    TemplateSelectorType = PersonaListItemType.Persona,
                    Data = new PersonaViewModel() { Title = "xPersonal Item 3" }
                });

                if (_contactSelected)
                {

                    listItems.Add(new PersonaListItemViewModel()
                    {
                        TemplateSelectorType = PersonaListItemType.Communicate,
                        Data = new CommunicateViewModel()
                        {
                            Label = "Send Persona and Call",
                            CallCommand = new Command(() =>
                            {
                                using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                                {
                                    if (_personaSelected)
                                    {
                                        // ToDo: SEND PERSONA!!!!!
                                        var dialService = commandScope.Resolve<IDialService>();
                                        dialService.Dial(telecommunicationService.CurrentSession.PhoneNumber);
                                    }
                                    else
                                    {

                                        var navigationService = commandScope.Resolve<INavigationService>();
                                        navigationService.CurrentPage.DisplayAlert("No Persona Selected Alert",
                                            "Please select a Persona to send from the Persona list", "OK");
                                    }
                                }
                            })

                        }
                    });
                    listItems.Add(new PersonaListItemViewModel()
                    {
                        TemplateSelectorType = PersonaListItemType.Communicate,
                        Data = new CommunicateViewModel()
                        {
                            Label = "Send Persona only",
                            CallCommand = new Command(() =>
                            {
                                using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                                {
                                    if (_personaSelected)
                                    {
                                        // ToDo: Send Persona !!!
                                        var navigationService = commandScope.Resolve<INavigationService>();
                                        navigationService.CurrentPage.DisplayAlert("Sent",
                                            "The selected Persona has been sent", "OK");
                                    }
                                    else
                                    {
                                        var navigationService = commandScope.Resolve<INavigationService>();
                                        navigationService.CurrentPage.DisplayAlert("No Persona Selected Alert",
                                            "Please select a Persona to send from the Persona list", "OK");
                                    }
                                }
                            })
                        }
                    });

                    listItems.Add(new PersonaListItemViewModel()
                    {
                        TemplateSelectorType = PersonaListItemType.Communicate,
                        Data = new CommunicateViewModel()
                        {
                            Label = "Call only",
                            CallCommand = new Command(() =>
                            {
                                using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                                {
                                    var dialService = scope.Resolve<IDialService>();
                                    dialService.Dial(telecommunicationService.CurrentSession.PhoneNumber);
                                }
                            })
                        }
                    });
                }

                this.PersonaListItems = listItems;
            }
        }
    }
}
