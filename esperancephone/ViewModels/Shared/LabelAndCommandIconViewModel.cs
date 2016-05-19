using System.Windows.Input;
using Com.Xamtastic.Patterns.SmallestMvvm;

namespace esperancephone.ViewModels.Shared
{
    public class LabelAndCommandIconViewModel : ViewModelBase
    {
        private bool _isSubItem;
        public bool IsSubItem
        {
            get { return _isSubItem; }
            set { _isSubItem = value; RaisePropertyChanged(); }
        }

        private string _labelText;
        public string LabelText
        {
            get { return _labelText; }
            set { _labelText = value; RaisePropertyChanged(); }
        }

        private string _iconCharacter;
        public string IconCharacter
        {
            get { return _iconCharacter; }
            set { _iconCharacter = value; RaisePropertyChanged(); }
        }

        private ICommand _command;
        public ICommand Command
        {
            get { return _command; }
            set { _command = value; RaisePropertyChanged(); }
        }
    }
}