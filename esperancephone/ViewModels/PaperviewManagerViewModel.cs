using Autofac;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using Xamarin.Forms;

namespace esperancephone.ViewModels
{
    public class PaperviewManagerViewModel : ModalViewModelBase
    {

        public PaperviewManagerViewModel()
        {
            this.Title = "Paperview Manager";

            this.CloseIconCharacter = "\uf00d";
            this.ContactIconCharacter = "\uf007";

            this.CloseCommand = new Command(async () =>
            {
                using (var scope = AppContainer.Container.BeginLifetimeScope())
                {
                    var navigationService = scope.Resolve<INavigationService>();
                    await navigationService.CurrentPage.Navigation.PopModalAsync();
                }
            });
        }
    }
}