using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Com.Xamtastic.Patterns.SmallestMvvm;

namespace esperancephone.ViewModels
{
    public class LabelAndCommandTextViewModel : ViewModelBase
    {
        private string _labelText;
        public string LabelText
        {
            get { return _labelText; }
            set { _labelText = value; RaisePropertyChanged(); }
        }

        private string _commandText;
        public string CommandText
        {
            get { return _commandText; }
            set { _commandText = value; RaisePropertyChanged(); }
        }

        private ICommand _command;

        public ICommand Command
        {
            get { return _command; }
            set { _command = value; RaisePropertyChanged(); }
        }
    }
}
