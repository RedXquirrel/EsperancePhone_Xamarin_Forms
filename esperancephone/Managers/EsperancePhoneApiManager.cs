using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.Interfaces;
using Microsoft.WindowsAzure.MobileServices;

namespace esperancephone.Managers
{
    public class EsperancePhoneApiManager : IEsperancePhoneApiManager
    {
        private MobileServiceClient _currentClient;

        public MobileServiceClient CurrentClient
        {
            get { return _currentClient; }
        }

        public EsperancePhoneApiManager()
        {
            this._currentClient = new MobileServiceClient(Constants.ApplicationURL);
        }
    }
}
