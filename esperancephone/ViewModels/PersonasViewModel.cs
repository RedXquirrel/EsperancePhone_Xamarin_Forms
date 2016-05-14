using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        }
                    });
                    listItems.Add(new PersonaListItemViewModel()
                    {
                        TemplateSelectorType = PersonaListItemType.Communicate,
                        Data = new CommunicateViewModel()
                        {
                            Label = "Send Persona only"
                        }
                    });

                    var dialService = scope.Resolve<IDialService>();
                    telecommunicationService.SetDialService(dialService);

                    listItems.Add(new PersonaListItemViewModel()
                    {
                        TemplateSelectorType = PersonaListItemType.Communicate,
                        Data = new CommunicateViewModel()
                        {
                            Label = "Call only",
                            CallCommand = new Command(() =>
                            {
                                dialService.Dial(telecommunicationService.CurrentSession.PhoneNumber);
                            })
                        }
                    });
                }

                this.PersonaListItems = listItems;
            }
        }
    }
}
