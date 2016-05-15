using System;
using System.Collections.Generic;
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
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class DiallerViewModel : StandardViewModel
    {
        private IDiallerService _diallerService;

        private ICommand _keyPressedCommand;
        public ICommand KeyPressedCommand
        {
            get { return _keyPressedCommand; }
            set { _keyPressedCommand = value; RaisePropertyChanged(); }
        }

        private ICommand _popCommand;
        public ICommand PopCommand
        {
            get { return _popCommand; }
            set { _popCommand = value; RaisePropertyChanged(); }
        }

        private IList<Keys> _keyStack;
        public IList<Keys> KeyStack
        {
            get { return _keyStack; }
            set { _keyStack = value; RaisePropertyChanged(); }
        }

        private string guid = Guid.NewGuid().ToString();

        public DiallerViewModel()
        {
            Debug.WriteLine($"INFORMATION: ViewModelGuid = {guid}");
            this.Title = "Esperance Dialler";

            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var diallerService = scope.Resolve<IDiallerService>();

                diallerService.CallAction = new Command<List<Keys>>(async (keys) =>
                {
                    if (keys != null && keys.Count != 0)
                    {
                        using (var commandScope = AppContainer.Container.BeginLifetimeScope())
                        {
                            var navigationService = commandScope.Resolve<INavigationService>();
                            var telecommunicationService = commandScope.Resolve<ITeleCommunicationService>();
                            var diallerServiceCS = commandScope.Resolve<IDiallerService>();
                            var commSession = new CommunicationModel();
                            commSession.DisplayName = "Unidentified";
                            commSession.PhoneNumber = diallerServiceCS.GetNumber(keys);
                            telecommunicationService.CurrentSession = commSession;
                            diallerServiceCS.PopAllkeys();
                            await navigationService.CurrentPage.Navigation.PushAsync(new PersonasPage());
                        }
                    }
                });
            }

            this.KeyPressedCommand = new Command<Keys>((key) =>
            {
                Debug.WriteLine($"INFORMATION: ViewModelGuid = {guid}");
                Debug.WriteLine($"INFORMATION: KEYPRESSEDCOMMAND: {key.ToString()}");
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var diallerService = scope.Resolve<IDiallerService>();
                    diallerService.Press(key);

                    if (key != Keys.KeyCall)
                    {
                        KeyStack = diallerService.GetStack().ToList();
                    }
                    else
                    {
                        //diallerService.PopAllkeys();
                    }
                }
            });

            this.PopCommand = new Command(() =>
            {
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var diallerService = scope.Resolve<IDiallerService>();
                    diallerService.PopKey();
                    KeyStack = diallerService.GetStack().ToList();
                }
            });
        }
    }
}
