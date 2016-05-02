using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Xamtastic.Patterns.SmallestMvvm;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class StandardViewModel : ViewModelBase
    {
        private string _title;

        public INavigation Navigator { get; internal set; }

        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }
    }
}
