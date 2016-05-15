using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.Models;

namespace esperancephone.Interfaces
{
    public interface ISettingsService
    {
        string ApplicationName { get; set; }
        string UserId { get; set; }
        string MobileServiceAuthenticationToken { get; set; }
        CurrentPageCacheModel CurrentPageCacheModel { get; set; }
    }
}
