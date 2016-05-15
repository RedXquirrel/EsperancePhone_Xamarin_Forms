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
    public class PaperviewsViewModel : StandardViewModel
    {
        private bool _contactSelected;
        private bool _paperviewSelected;

        private PaperviewListItemViewModel _selectedListItem;

        public ICommand SelectedPersonaListItemCommand => new Command<PaperviewListItemViewModel>((item) =>
        {
            Debug.WriteLine($"INFORMATION: Selected Paperview List Data Item class is {item.Data.GetType().ToString()}");

            if (item.Data.GetType() == typeof (PaperviewViewModel))
            {
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    SetSelectedItem(item);

                    var paperviewService = scope.Resolve<IPaperviewService>();

                    paperviewService.CurrentPaperview = _paperviewSelected
                        ? ((PaperviewViewModel) item.Data).Paperview
                        : null;

                    var contactsService = scope.Resolve<IContactsService>();
                    var telecommunicationService = scope.Resolve<ITeleCommunicationService>();
                    //if (contactsService.CurrentContact == null)
                    if (telecommunicationService.CurrentSession == null)
                    {
                        var navigationService = scope.Resolve<INavigationService>();
                        navigationService.CurrentPage.Navigation.PushAsync(new ContactsPage());
                    }
                }
            }
        });

        private void SetSelectedItem(PaperviewListItemViewModel item)
        {
            _selectedListItem = item;
            if (_selectedListItem != null)
            {
                _paperviewSelected = item.Data.GetType() == typeof (PaperviewViewModel);
            }
        }

        private ObservableCollection<PaperviewListItemViewModel> _personaListItems;
        public ObservableCollection<PaperviewListItemViewModel> PersonaListItems
        {
            get { return _personaListItems; }
            set { _personaListItems = value; RaisePropertyChanged(); }
        }

        public PaperviewsViewModel()
        {
            this.Title = "Paperviews";

            BuildDataItems();
        }

        private async void BuildDataItems()
        {
            var listItems = new ObservableCollection<PaperviewListItemViewModel>();

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var telecommunicationService = scope.Resolve<ITeleCommunicationService>();
                //var dialService = scope.Resolve<IDialService>();
                //telecommunicationService.SetDialService(dialService);

                var paperviewService = scope.Resolve<IPaperviewService>();
                if (paperviewService.CurrentPaperview != null)
                {
                    _paperviewSelected = true;
                }
                
                _contactSelected = telecommunicationService.CurrentSession != null ? true : false;

                if (_contactSelected)
                {
                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.DisplayName,
                        Data = new DisplayNameViewModel() { DisplayName = telecommunicationService.CurrentSession.DisplayName }
                    });

                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.PhoneNumber,
                        Data = new PhoneNumberViewModel() { PhoneNumber = telecommunicationService.CurrentSession.PhoneNumber }
                    });
                }

                if (_paperviewSelected)
                {
                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.PaperviewsGroupHeading,
                        Data = new PersonasGroupHeadingViewModel()
                        {
                            LabelText = "Selected Paperview:",
                            IconCharacter = "\uf147",
                            AddCommand = new Command(async() =>
                            {
                                using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                                {
                                    var paperviewServiceCs = commandScope.Resolve<IPaperviewService>();
                                    paperviewServiceCs.CurrentPaperview = null;

                                    _paperviewSelected = false;

                                    var index = 2;

                                    PersonaListItems.RemoveAt(index); // Label

                                    PersonaListItems.RemoveAt(index); // Paperview item

                                    PersonaListItems.Insert(index, GetSelectRow());

                                    var list = await BuildPaperviewViewModels();

                                    foreach (var item in list)
                                    {
                                        PersonaListItems.Insert(++index,item);
                                    }

                                }
                            })
                        }
                    });

                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.Paperviews,
                        Data = new PaperviewViewModel()
                        {
                            Paperview = paperviewService.CurrentPaperview
                        }
                    });

                }
                else
                {
                    
                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.PaperviewsGroupHeading,
                        Data = new PersonasGroupHeadingViewModel()
                        {
                            LabelText = "Select Paperview:",
                            IconCharacter = "\uf196",
                            AddCommand = new Command(() =>
                            {
                                using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                                {
                                    var navigationService = commandScope.Resolve<INavigationService>();
                                    navigationService.CurrentPage.DisplayAlert("ToDo",
                                        "To be implemented", "OK");
                                }
                            })
                        }
                    });

                    var list = await BuildPaperviewViewModels();

                    foreach (var item in list)
                    {
                        listItems.Add(item);
                    }
                   
                }

                if (_contactSelected)
                {

                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.Communicate,
                        Data = new CommunicateViewModel()
                        {
                            Label = "Send Paperview and Call",
                            CallCommand = new Command(() =>
                            {
                                using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                                {
                                    if (_paperviewSelected)
                                    {
                                        // ToDo: SEND PERSONA!!!!!
                                        var dialService = commandScope.Resolve<IDialService>();
                                        dialService.Dial(telecommunicationService.CurrentSession.PhoneNumber);
                                    }
                                    else
                                    {
                                        var navigationService = commandScope.Resolve<INavigationService>();
                                        navigationService.CurrentPage.DisplayAlert("No Paperview Selected Alert",
                                            "Please select a Paperview to send from the Paperview list", "OK");
                                    }
                                }
                            })

                        }
                    });
                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.Communicate,
                        Data = new CommunicateViewModel()
                        {
                            Label = "Send Paperview only",
                            CallCommand = new Command(() =>
                            {
                                using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                                {
                                    if (_paperviewSelected)
                                    {
                                        // ToDo: Send Persona !!!
                                        var navigationService = commandScope.Resolve<INavigationService>();
                                        navigationService.CurrentPage.DisplayAlert("Sent",
                                            "The selected Paperview has been sent", "OK");
                                    }
                                    else
                                    {
                                        var navigationService = commandScope.Resolve<INavigationService>();
                                        navigationService.CurrentPage.DisplayAlert("No Paperview Selected Alert",
                                            "Please select a Paperview to send from the Paperview list", "OK");
                                    }
                                }
                            })
                        }
                    });

                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.Communicate,
                        Data = new CommunicateViewModel()
                        {
                            Label = "Call only",
                            CallCommand = new Command(() =>
                            {
                                using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                                {
                                    var dialService = commandScope.Resolve<IDialService>();
                                    dialService.Dial(telecommunicationService.CurrentSession.PhoneNumber);
                                }
                            })
                        }
                    });
                }

                this.PersonaListItems = listItems;
            }
        }

        private static PaperviewListItemViewModel GetSelectRow()
        {
            return new PaperviewListItemViewModel()
            {
                TemplateSelectorType = PaperviewListItemType.PaperviewsGroupHeading,
                Data = new PersonasGroupHeadingViewModel()
                {
                    LabelText = "Select Paperview:",
                    IconCharacter = "\uf196",
                    AddCommand = new Command(() =>
                    {
                        using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                        {
                            var navigationService = commandScope.Resolve<INavigationService>();
                            navigationService.CurrentPage.DisplayAlert("ToDo",
                                "To be implemented", "OK");
                        }
                    })
                }
            };
        }

        private async Task<List<PaperviewListItemViewModel>> BuildPaperviewViewModels()
        {

            // ToDo: GET THESE PROPERLY, FAKE IT FOR NOW

            var list = new List<PaperviewListItemViewModel>();
            list.Add(new PaperviewListItemViewModel()
            {
                TemplateSelectorType = PaperviewListItemType.Paperviews,
                Data = new PaperviewViewModel()
                {
                    Paperview = new PaperviewModel()
                    {
                        DisplayName = "xPersonal Item 0"
                    }
                }
            });
            list.Add(new PaperviewListItemViewModel()
            {
                TemplateSelectorType = PaperviewListItemType.Paperviews,
                Data = new PaperviewViewModel()
                {
                    Paperview = new PaperviewModel()
                    {
                        DisplayName = "xPersonal Item 1"
                    }
                }
            });
            list.Add(new PaperviewListItemViewModel()
            {
                TemplateSelectorType = PaperviewListItemType.Paperviews,
                Data = new PaperviewViewModel()
                {
                    Paperview = new PaperviewModel()
                    {
                        DisplayName = "xPersonal Item 2"
                    }
                }
            });
            list.Add(new PaperviewListItemViewModel()
            {
                TemplateSelectorType = PaperviewListItemType.Paperviews,
                Data = new PaperviewViewModel()
                {
                    Paperview = new PaperviewModel()
                    {
                        DisplayName = "xPersonal Item 3"
                    }
                }
            });
            return list;
        }
    }
}
