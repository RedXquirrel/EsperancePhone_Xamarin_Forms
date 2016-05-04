using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.Helpers;
using esperancephone.Interfaces;

namespace esperancephone.Services
{
    public class SettingsService : ISettingsService
    {
        public string ApplicationName
        {
            get { return Settings.ApplicationName; }
            set { Settings.ApplicationName = value; }
        }

        public string UserId
        {
            get { return Settings.UserId; }
            set { Settings.UserId = value; }
        }

        public string MobileServiceAuthenticationToken
        {
            get { return Settings.MobileServiceAuthenticationToken; }
            set { Settings.MobileServiceAuthenticationToken = value; }
        }
    }
}
