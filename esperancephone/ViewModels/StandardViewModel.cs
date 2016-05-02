using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Interfaces;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class StandardViewModel : ViewModelBase
    {
        private ISettingsService _settingsService;

        public INavigation Navigator { get; internal set; }

        public string ApplicationName => _settingsService.ApplicationName;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }

        public StandardViewModel()
        {
            
        }

        public StandardViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

    }
}
