using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Autofac;
using esperancephone.DataSources;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Models;

namespace esperancephone.ViewModels
{
    public class ContactsViewModel : StandardViewModel
    {
        private ObservableCollection<IContact> _contacts;
        public ObservableCollection<IContact> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; RaisePropertyChanged(); }
        }

        public IList<ContactsGroupDataSource> ContactGroups => ContactsGroupDataSource.Groups;

        public ContactsViewModel()
        {
            this.Title = "Contacts";
            GetContacts();
        }

        private async void GetContacts()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IContactsService>();
                var contacts = await service.GetContacts();
                _contacts = new ObservableCollection<IContact>(contacts);
            }
        }

    }
}