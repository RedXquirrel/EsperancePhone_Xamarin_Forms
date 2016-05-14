using Com.Xamtastic.Patterns.SmallestMvvm;

namespace esperancephone.ViewModels.Shared
{
    public class DisplayNameViewModel : ViewModelBase
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; RaisePropertyChanged(); }
        }
    }
}
