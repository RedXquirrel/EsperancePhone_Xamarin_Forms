using Com.Xamtastic.Patterns.SmallestMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esperancephone.ViewModels
{
    public class MasterPageIndexItemViewModel : ViewModelBase
    {
        private string _iconKey;

        /// <summary>
        /// The unicode icon-font key, eg "\uf0c5"
        /// </summary>
        public string IconKey
        {
            get { return _iconKey; }
            set { _iconKey = value; RaisePropertyChanged(); }
        }

        private string _title;
        /// <summary>
        /// The Index Item text
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }

        private string _subtitle;
        /// <summary>
        /// The Index Item sub-text
        /// </summary>
        public string SubTitle
        {
            get { return _subtitle; }
            set { _subtitle = value; RaisePropertyChanged(); }
        }

        private Type _pageType;
        /// <summary>
        /// The name of the ViewModel to navigate to
        /// </summary>
        public Type PageType
        {
            get { return _pageType; }
            set { _pageType = value; RaisePropertyChanged(); }
        }

        private Action _action;
        /// <summary>
        /// The action to invoke when this ViewModel is processed.
        /// </summary>
        public Action Action
        {
            get { return _action; }
            set { _action = value; RaisePropertyChanged(); }
        }

        private bool _isPage;
        /// <summary>
        /// Does the item represent a Page of a Page Function (i.e. navigate to page but pass parameter), used for DataTemplate selection
        /// </summary>
        // ToDo: Change this to an enumeration
        public bool IsPage
        {
            get { return _isPage; }
            set { _isPage = value; RaisePropertyChanged(); }
        }
    }
}
