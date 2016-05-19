using System;
using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Interfaces;

namespace esperancephone.ViewModels
{
    public enum ContactsListItemItemTemplates
    {
        Personant,
        NonPersonant,
        Paperview,
        Dialler
    }
    public class ContactsListItemViewModel : ViewModelBase
    {
        public IContact Contact { get; set; }

        private string _iconKey;
        /// <summary>
        /// The unicode icon-font key, eg "\uf0c5"
        /// </summary>
        public string IconKey
        {
            get { return _iconKey; }
            set { _iconKey = value; RaisePropertyChanged(); }
        }

        private string _displayName;
        /// <summary>
        /// The Contact's Display Name.
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; RaisePropertyChanged(); }
        }

        private string _firstName;
        /// <summary>
        /// The Contact's FirstName
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; RaisePropertyChanged(); }
        }

        private string _lastName;
        /// <summary>
        /// The Contact's LastName
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; RaisePropertyChanged(); }
        }

        private ContactsListItemItemTemplates _listItemType;
        /// <summary>
        /// Does the contact have Personant artefacts
        /// </summary>
        public ContactsListItemItemTemplates ListItemType
        {
            get { return _listItemType; }
            set { _listItemType = value; RaisePropertyChanged(); }
        }

        public object Data { get; set; }

    }
}