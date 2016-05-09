using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using esperancephone.Extensions;
using esperancephone.Services;
using Xamarin.Forms;

namespace esperancephone.Pages
{
    public partial class MainIndexMasterPage : ContentPage
    {
        public static NavigationPage MainNavigationPage { get; set; }

        public ListView ListView { get { return listView; } }

        public MainIndexMasterPage()
        {
            InitializeComponent();

            ListView.ItemsSource = MasterPageIndexService.Index;
        }
    }
}
