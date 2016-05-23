using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.Helpers;
using esperancephone.Interfaces;
using esperancephone.Models;
using Newtonsoft.Json;

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

        public UserMode UserMode
        {
            get { return Settings.UserMode; }
            set { Settings.UserMode = value; }
        }

        public string MobileServiceAuthenticationToken
        {
            get { return Settings.MobileServiceAuthenticationToken; }
            set { Settings.MobileServiceAuthenticationToken = value; }
        }

        public CurrentPageCacheModel CurrentPageCacheModel
        {
            get { return JsonConvert.DeserializeObject<CurrentPageCacheModel>(Settings.CurrentPageCacheModel); }
            set { Settings.CurrentPageCacheModel = JsonConvert.SerializeObject(value); }
        }
    }
}
