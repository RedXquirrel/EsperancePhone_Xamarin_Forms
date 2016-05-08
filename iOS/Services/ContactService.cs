using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using esperancephone.Interfaces;
using esperancephone.Models;

namespace esperancephone.iOS.Services
{
    public class ContactService : IContactsService
    {
        private readonly Xamarin.Contacts.AddressBook _book;

        private static IEnumerable<Contact> _contacts;

        public ContactService()
        {
            _book = new Xamarin.Contacts.AddressBook();
        }

        public List<IContact> Find(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IContact>> GetContacts()
        {
            if (_contacts != null) return _contacts;

            var contacts = new List<Contact>();

            await _book.RequestPermission().ContinueWith(t =>
            {
                if (!t.Result)
                {
                    Console.WriteLine("Sorry ! Permission was denied by user or manifest !");
                    return;
                }

                foreach (var contact in _book.Where(c => c.Phones.Any()))
                {
                    var phones = contact.Phones.Select(phone => new Phone() {Label = phone.Label, Number = phone.Number}).ToList();

                    contacts.Add(new Contact() { DisplayName = contact.DisplayName, FirstName = contact.FirstName, LastName = contact.LastName, Phones = phones });
                }

                _contacts = contacts.OrderBy(c => c.DisplayName);

            });

            return _contacts;
        }
    }
}