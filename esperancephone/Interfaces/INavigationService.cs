using System;
using System.Reflection;
using Xamarin.Forms;

namespace esperancephone.Interfaces
{
    public interface INavigationService
    {
        INavigation RootNavigation { get; set; }

        INavigation Navigation { get; set; }

        Page CurrentPage { get; set; }

        void Show(Type pageType);

        Action<bool> MasterDetailAction { get; set; }
        bool MasterDetailIsOpen { get; set; }
    }
}