using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Interfaces;

namespace esperancephone.ViewModels.ContactListItemViewModels
{
    public class PhoneViewModel : ViewModelBase, IPhone
    {
        private string _label;
        public string Label
        {
            get { return _label; }
            set { _label = value; RaisePropertyChanged(); }
        }

        private string _number;

        public string Number
        {
            get { return _number; }
            set { _number = value; RaisePropertyChanged(); }
        }

        private string _callIcon;
        public string CallIcon
        {
            get { return _callIcon; }
            set { _callIcon = value; RaisePropertyChanged(); }
        }

        private ICommand _callCommand;

        public ICommand CallCommand
        {
            get { return _callCommand; }
            set { _callCommand = value; RaisePropertyChanged(); }
        }

    }
}
