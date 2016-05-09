using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Com.Xamtastic.Patterns.SmallestMvvm;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class ContactViewModel : StandardViewModel
    {
        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value; RaisePropertyChanged(); }
        }

        private string _closeIconCharacter;

        public string CloseIconCharacter
        {
            get { return _closeIconCharacter; }
            set
            {
                _closeIconCharacter = value;
                RaisePropertyChanged();
            }
        }

        public ContactViewModel()
        {
            this.Title = "Contact";
            this.CloseIconCharacter = "\uf00d";

            this.CloseCommand = new Command(async () =>
            {
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var service = scope.Resolve<INavigationService>();

                    await service.CurrentPage.Navigation.PopModalAsync();
                }
            });
        }
    }
}
