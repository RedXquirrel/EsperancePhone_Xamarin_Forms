using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Com.Xamtastic.Patterns.SmallestMvvm;

namespace esperancephone.ViewModels
{
    public class PersonasGroupHeadingViewModel : ViewModelBase
    {
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

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; RaisePropertyChanged(); }
        }
    }
}
