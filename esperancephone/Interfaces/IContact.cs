using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.Models;

namespace esperancephone.Interfaces
{
    public interface IContact
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string DisplayName { get; set; }
        string EmailAddress { get; set; }
        IEnumerable<Phone> Phones { get; set; }
    }
}
