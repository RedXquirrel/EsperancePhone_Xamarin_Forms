using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.Interfaces;
using Xamarin.Forms;

namespace esperancephone.Services
{
    public class NavigationService : INavigationService
    {
        public INavigation RootNavigation { get; set; }

        public INavigation Navigation { get; set; }
        public Page CurrentPage { get; set; }

        public void Show(Type pageType)
        {
            
        }

        public Action<bool> MasterDetailAction { get; set; }
        public bool MasterDetailIsOpen { get; set; }
    }
}
