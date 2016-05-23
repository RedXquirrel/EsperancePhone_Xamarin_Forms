using esperancephone.Models;

namespace esperancephone.ViewModels.PersonaListItemViewModels
{
    public class PaperviewViewModel : StandardViewModel
    {
        private PaperviewModel _paperview;

        public PaperviewModel Paperview
        {
            get { return _paperview; }
            set { _paperview = value; this.Title = _paperview.DisplayName; }
        }

        public PaperviewViewModel()
        {
            this.Title = "Paperview Item";
        }
         
    }
}