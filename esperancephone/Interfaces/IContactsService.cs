using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esperancephone.Interfaces
{
    public interface IContactsService
    {
        Task<IEnumerable<IContact>> GetContacts();
        List<IContact> Find(string searchTerm);
        IContact CurrentContact { get; set; }
    }
}
