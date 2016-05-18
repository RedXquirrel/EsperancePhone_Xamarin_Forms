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
        private bool _isCommSession;
        private bool _isPaperviewSelected;

        private int _listAnimationDelay = 105;

        private PaperviewListItemViewModel _selectedListItem;

        public ICommand SelectedPaperviewListItemCommand => new Command<PaperviewListItemViewModel>(async(item) =>
        {
            Debug.WriteLine($"INFORMATION: Selected Paperview List Data Item class is {item.Data.GetType().ToString()}");

            if (item.Data.GetType() == typeof (PaperviewViewModel))
            {
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    _selectedListItem = item;
                    if (_selectedListItem != null)
                    {
                        _isPaperviewSelected = item.Data.GetType() == typeof(PaperviewViewModel);
                    }

                    var paperviewService = scope.Resolve<IPaperviewService>();

                    paperviewService.CurrentPaperview = _isPaperviewSelected
                        ? ((PaperviewViewModel) item.Data).Paperview
                        : null;

                    var contactsService = scope.Resolve<IContactsService>();
                    var telecommunicationService = scope.Resolve<ITeleCommunicationService>();
                    //if (contactsService.CurrentContact == null)
                    if (telecommunicationService.CurrentSession == null)
                    {
                        var navigationService = scope.Resolve<INavigationService>();
                        navigationService.CurrentPage.Navigation.PushAsync(new ContactsPage(), false);
                    }
                    else
                    {
                        int lastX = 0;
                        for (var x = this.PersonaListItems.Count; x > 0; x--)
                        {
                            if (this.PersonaListItems[x - 1].Data.GetType() == typeof(PaperviewViewModel))
                            {
                                await Task.Delay(_listAnimationDelay);
                                this.PersonaListItems.RemoveAt(x - 1);
                                lastX = x;
                            }
                        }
                        await Task.Delay(_listAnimationDelay);
                        this.PersonaListItems.RemoveAt(lastX-2); // Remove the heading
                        await Task.Delay(_listAnimationDelay);
                        this.PersonaListItems.Insert(lastX-2, GetAlreadySelectedHeadingListItem()); // Insert the new heading
                        await Task.Delay(_listAnimationDelay);
                        this.PersonaListItems.Insert(lastX-1, GetAlreadySelectedPaperviewListItem()); // Insert the newly selected item
                    }
                }
            }
        });


        private void SetSelectedItem(PaperviewListItemViewModel item)
        {
            _selectedListItem = item;
            if (_selectedListItem != null)
            {
                _isPaperviewSelected = item.Data.GetType() == typeof (PaperviewViewModel);
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
            // Declare the list to build. It's all about building this list !!!
            var listItems = new ObservableCollection<PaperviewListItemViewModel>();

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var telecommunicationService = scope.Resolve<ITeleCommunicationService>();
                
                _isCommSession = telecommunicationService.CurrentSession != null ? true : false;

                if (_isCommSession) // i.e. Has a previous Display Name and Number been selected?
                {
                    // Add DisplayName
                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.LabelAndCommandText,
                        Data = GetLabelAndCommandLabelViewModel()
                    });

                    // Add PhoneNumber
                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.PhoneNumber,
                        Data = GetPhoneNumberViewModel()
                    });
                }

                var paperviewService = scope.Resolve<IPaperviewService>();

                if (paperviewService.CurrentPaperview != null)
                {
                    _isPaperviewSelected = true;
                }

                if (_isPaperviewSelected) // i.e. Has a Paperview been selected
                {
                    // Add a heading to display the Paperview item under
                    listItems.Add(GetAlreadySelectedHeadingListItem());

                    // Add the Already Selected Paperview item under the above heading
                    listItems.Add(GetAlreadySelectedPaperviewListItem());
                }
                else // i.e. Paperview has not been selected
                {
                    await BuildAllPaperviewListItems(listItems);
                }

                if (_isCommSession)
                {

                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.Communicate,
                        Data = GetDeliverPaperviewThenCallViewModel()
                    });

                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.Communicate,
                        Data = GetSendPaperviewAndCallViewModel()
                    });

                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.Communicate,
                        Data = GetSendPaperviewOnlyViewModel()
                    });

                    listItems.Add(new PaperviewListItemViewModel()
                    {
                        TemplateSelectorType = PaperviewListItemType.Communicate,
                        Data = GetCallOnlyViewModel()
                    });
                }

                this.PersonaListItems = listItems;
            }
        }

        private static PaperviewListItemViewModel GetAlreadySelectedPaperviewListItem()
        {
            return new PaperviewListItemViewModel()
            {
                TemplateSelectorType = PaperviewListItemType.Paperviews,
                Data = GetCurrentPaperviewViewModel()
            };
        }

        private PaperviewListItemViewModel GetAlreadySelectedHeadingListItem()
        {
            return new PaperviewListItemViewModel()
            {
                TemplateSelectorType = PaperviewListItemType.LabelAndCommandText,
                Data = GetPaperviewAlreadySelectedViewModel()
            };
        }

        private async Task BuildAllPaperviewListItems(ObservableCollection<PaperviewListItemViewModel> listItems)
        {
            await Task.Delay(_listAnimationDelay);
            // Add a heading to disp[lay ALL the Paperviews under
            listItems.Add(new PaperviewListItemViewModel()
            {
                TemplateSelectorType = PaperviewListItemType.LabelAndCommandText,
                Data = GetPleaseSelectPaperviewViewModel()
            });

            // Add all the available Paperviews under the above heading
            foreach (var item in await BuildPaperviewViewModels())
            {
                await Task.Delay(_listAnimationDelay);
                listItems.Add(item);
            }
        }

        #region Private methods to create the ViewModels for each particular row in the list

        private static PaperviewListItemViewModel GetPleaseSelectPaperviewHeadingRow()
        {
            return new PaperviewListItemViewModel()
            {
                TemplateSelectorType = PaperviewListItemType.LabelAndCommandText,
                Data = GetPleaseSelectPaperviewViewModel()
            };
        }

        private static LabelAndCommandTextViewModel GetPleaseSelectPaperviewViewModel()
        {
            return new LabelAndCommandTextViewModel()
            {
                LabelText = "Select Paperview:",
                CommandText = "new",
                Command = new Command(async () =>
                {
                    using (var scope = AppContainer.Container.BeginLifetimeScope())
                    {
                        var navigationService = scope.Resolve<INavigationService>();
                        await navigationService.CurrentPage.Navigation.PushModalAsync(new PaperviewManagerPage());
                    }
                })
            };
        }

        private LabelAndCommandTextViewModel GetPaperviewAlreadySelectedViewModel()
        {
            return new LabelAndCommandTextViewModel()
            {
                LabelText = "Selected Paperview:",
                CommandText = "remove",
                Command = new Command(async () =>
                {
                    using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                    {
                        var paperviewServiceCs = commandScope.Resolve<IPaperviewService>();
                        paperviewServiceCs.CurrentPaperview = null;

                        _isPaperviewSelected = false;

                        var index = 2;

                        PersonaListItems.RemoveAt(index); // Label
                            await Task.Delay(_listAnimationDelay);
                        PersonaListItems.RemoveAt(index); // Paperview item
                            await Task.Delay(_listAnimationDelay);
                        PersonaListItems.Insert(index, GetPleaseSelectPaperviewHeadingRow());

                        var list = await BuildPaperviewViewModels();

                        foreach (var item in list)
                        {
                            await Task.Delay(_listAnimationDelay);
                            PersonaListItems.Insert(++index, item);
                        }

                    }
                })

            };
        }

        private LabelAndCommandTextViewModel GetLabelAndCommandLabelViewModel()
        {
            string displayName = string.Empty;

            using (var commandScope = AppContainer.Container.BeginLifetimeScope())
            {
                var telecommunicationsService = commandScope.Resolve<ITeleCommunicationService>();
                displayName = telecommunicationsService.CurrentSession.DisplayName;
            }

            return new LabelAndCommandTextViewModel()
            {
                LabelText = displayName,
                CommandText = "clear",
                Command = new Command(async() =>
                {
                    using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                    {
                        var telecommunicationService = commandScope.Resolve<ITeleCommunicationService>();

                        telecommunicationService.CurrentSession = null;

                        var paperviewService = commandScope.Resolve<IPaperviewService>();

                        paperviewService.CurrentPaperview = null;

                        while (PersonaListItems.Count != 0)
                        {
                            await Task.Delay(_listAnimationDelay);
                            PersonaListItems.RemoveAt(PersonaListItems.Count - 1);

                        }

                        await BuildAllPaperviewListItems(PersonaListItems);
                    }
                })
            };
        }

        private static DisplayNameViewModel GetDisplayNameViewModel()
        {
            string displayName = string.Empty;

            using (var commandScope = AppContainer.Container.BeginLifetimeScope())
            {
                var telecommunicationsService = commandScope.Resolve<ITeleCommunicationService>();
                displayName = telecommunicationsService.CurrentSession.DisplayName;
            }

            return new DisplayNameViewModel()
            {
                DisplayName = displayName
            };
        }

        private static PhoneNumberViewModel GetPhoneNumberViewModel()
        {
            string phoneNumber = string.Empty;

            using (var commandScope = AppContainer.Container.BeginLifetimeScope())
            {
                var telecommunicationsService = commandScope.Resolve<ITeleCommunicationService>();
                phoneNumber = telecommunicationsService.CurrentSession.PhoneNumber;
            }

            return new PhoneNumberViewModel()
            {
                PhoneNumber = phoneNumber
            };
        }

        private static PaperviewModel GetCurrentPaperviewModel()
        {
            PaperviewModel response = null;

            using (var commandScope = AppContainer.Container.BeginLifetimeScope())
            {
                var paperviewService = commandScope.Resolve<IPaperviewService>();
                response = paperviewService.CurrentPaperview;
            }
            return response;
        }

        private static PaperviewViewModel GetCurrentPaperviewViewModel()
        {
            return new PaperviewViewModel()
            {
                Paperview = GetCurrentPaperviewModel()
            };
        }

        private CommunicateViewModel GetDeliverPaperviewThenCallViewModel()
        {
            return new CommunicateViewModel()
            {
                Label = "Deliver Paperview then Call",
                CallCommand = new Command(async() =>
                {
                    using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                    {
                        if (_isPaperviewSelected)
                        {
                            // ToDo: SEND PERSONA!!!!!

                            this.IsBusy = true;
                            var dialService = commandScope.Resolve<IDialService>();
                            var telecommunicationService = commandScope.Resolve<ITeleCommunicationService>();
                            await Task.Delay(8000);
                            this.IsBusy = false;
                            dialService.Dial(telecommunicationService.CurrentSession.PhoneNumber);
                        }
                        else
                        {
                            var navigationService = commandScope.Resolve<INavigationService>();
                            await navigationService.CurrentPage.DisplayAlert("No Paperview Selected Alert",
                                "Please select a Paperview to send from the Paperview list", "OK");
                        }
                    }
                })

            };
        }

        private CommunicateViewModel GetSendPaperviewAndCallViewModel()
        {
            return new CommunicateViewModel()
            {
                Label = "Send Paperview and Call",
                CallCommand = new Command(() =>
                {
                    using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                    {
                        if (_isPaperviewSelected)
                        {
                            // ToDo: SEND PERSONA!!!!!
                            var dialService = commandScope.Resolve<IDialService>();
                            var telecommunicationService = commandScope.Resolve<ITeleCommunicationService>();
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

            };
        }

        private CommunicateViewModel GetSendPaperviewOnlyViewModel()
        {
            return new CommunicateViewModel()
            {
                Label = "Send Paperview only",
                CallCommand = new Command(() =>
                {
                    using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                    {
                        if (_isPaperviewSelected)
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
            };
        }

        private CommunicateViewModel GetCallOnlyViewModel()
        {
            return new CommunicateViewModel()
            {
                Label = "Call only",
                CallCommand = new Command(() =>
                {
                    using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                    {
                        var dialService = commandScope.Resolve<IDialService>();
                        var telecommunicationService = commandScope.Resolve<ITeleCommunicationService>();
                        dialService.Dial(telecommunicationService.CurrentSession.PhoneNumber);
                    }
                })
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
        #endregion
    }
}
