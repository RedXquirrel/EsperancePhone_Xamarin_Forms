using esperancephone.Models;

namespace esperancephone.ViewModels.SettingListItemViewModels
{
    public class SettingViewModel : StandardViewModel
    {
        private SettingModel _setting;

        public SettingModel Setting
        {
            get { return _setting; }
            set { _setting = value; this.Title = _setting.DisplayName; }
        }

        public SettingViewModel()
        {
            this.Title = "Setting Item";
        }

    }
}