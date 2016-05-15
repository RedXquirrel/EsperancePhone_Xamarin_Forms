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
        private bool _personaSelected;

        private PaperviewListItemViewModel _selectedListItem;

        public ICommand SelectedPersonaListItemCommand => new Command<PaperviewListItemViewModel>((item) =>
        {
            Debug.WriteLine($"INFORMATION: Selected Paperview List Data Item class is {item.Data.GetType().ToString()}");

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                SetSelectedItem(item);
            }
        });

        private void SetSelectedItem(PaperviewListItemViewModel item)
        {
            _selectedListItem = item;
            if (_selectedListItem != null)
            {
                _personaSelected = item.Data.GetType() == typeof (PaperviewViewModelViewModel);
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

        private void BuildDataItems()
        {
            var listItems = new ObservableCollection<PaperviewListItemViewModel>();

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var telecommunicationService = scope.Resolve<ITeleCommunicationService>();
                //var dialService = scope.Resolve<IDialService>();
                //telecommunicationService.SetDialService(dialService);

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

                listItems.Add(new PaperviewListItemViewModel()
                {
                    TemplateSelectorType = PaperviewListItemType.Paperviews,
                    Data = new PaperviewViewModelViewModel()
                    {
                        Paperview = new PaperviewModel()
                        {
                            DisplayName = "xPersonal Item 0"
                        }
                    }
                });
                listItems.Add(new PaperviewListItemViewModel()
                {
                    TemplateSelectorType = PaperviewListItemType.Paperviews,
                    Data = new PaperviewViewModelViewModel()
                    {
                        Paperview = new PaperviewModel()
                        {
                            DisplayName = "xPersonal Item 1"
                        }
                    }
                });
                listItems.Add(new PaperviewListItemViewModel()
                {
                    TemplateSelectorType = PaperviewListItemType.Paperviews,
                    Data = new PaperviewViewModelViewModel()
                    {
                        Paperview = new PaperviewModel()
                        {
                            DisplayName = "xPersonal Item 2"
                        }
                    }
                });
                listItems.Add(new PaperviewListItemViewModel()
                {
                    TemplateSelectorType = PaperviewListItemType.Paperviews,
                    Data = new PaperviewViewModelViewModel()
                    {
                        Paperview = new PaperviewModel()
                        {
                            DisplayName = "xPersonal Item 3"
                        }
                    }
                });

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
                                    if (_personaSelected)
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
                                    if (_personaSelected)
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
    }
}
