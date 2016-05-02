using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.ViewModels;
using Xamarin.Forms;

namespace esperancephone.Pages
{
    [ViewModelType(typeof(LoginViewModel))]
    public partial class LoginPage : PageBase
    {
        public LoginPage()
        {
            InitializeComponent();

            Debug.WriteLine($"INFORMATION: ViewModelType is {this.BindingContext.GetType().Name}");

            ((StandardViewModel) this.BindingContext).Navigator = (INavigation) this.Navigation;
        }
    }
}
