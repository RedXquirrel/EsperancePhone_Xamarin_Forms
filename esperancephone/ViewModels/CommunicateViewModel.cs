using System.Collections.Generic;
using System.Windows.Input;
using Com.Xamtastic.Patterns.SmallestMvvm;

namespace esperancephone.ViewModels
{
    public class CommunicateViewModel : ViewModelBase
    {
        private ICommand _callCommand;
        public ICommand CallCommand
        {
            get { return _callCommand; }
            set { _callCommand = value; RaisePropertyChanged(); }
        }

        private ICommand _callAndPersonaCommand;
        public ICommand CallAndPersonaCommand
        {
            get { return _callAndPersonaCommand; }
            set { _callAndPersonaCommand = value; RaisePropertyChanged(); }
        }

        private ICommand _personaCommand;
        public ICommand PersonaCommand
        {
            get { return _personaCommand; }
            set { _personaCommand = value; RaisePropertyChanged(); }
        }

        private string _label;
        public string Label
        {
            get { return _label; }
            set { _label = value; RaisePropertyChanged(); }
        }


    }
}