using KamiinaBotan.Core;
using KamiinaBotan.Services;

namespace KamiinaBotan.ViewModels
{
    public class StageMenuViewModel : ViewModelBase
    {
        public string BackgroundPath => "pack://application:,,,/images/StageScreen.jpg";
        private readonly INavigationService _navigation;

        public RelayCommand BackCommand { get; }

        public StageMenuViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            BackCommand = new RelayCommand(_ => _navigation.NavigateTo<StartMenuViewModel>());
        }
    }
}