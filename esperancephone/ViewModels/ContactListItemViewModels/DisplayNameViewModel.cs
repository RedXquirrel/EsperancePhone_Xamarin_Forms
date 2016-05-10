using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Xamtastic.Patterns.SmallestMvvm;

namespace esperancephone.ViewModels.ContactListItemViewModels
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
