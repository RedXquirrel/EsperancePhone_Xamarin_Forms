using System.Windows.Input;

namespace esperancephone.ViewModels
{
    public class ModalViewModelBase : StandardViewModel
    {
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

    }
}