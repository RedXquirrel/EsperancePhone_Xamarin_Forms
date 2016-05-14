using Com.Xamtastic.Patterns.SmallestMvvm;

namespace esperancephone.ViewModels.PersonaListItemViewModels
{
    public class PhoneNumberViewModel : ViewModelBase
    {
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; RaisePropertyChanged(); }
        }
    }
}