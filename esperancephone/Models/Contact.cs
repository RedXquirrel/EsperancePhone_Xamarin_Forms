using System.Collections.Generic;
using esperancephone.Interfaces;

namespace esperancephone.Models
{
    public class Contact : IContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
    }
}