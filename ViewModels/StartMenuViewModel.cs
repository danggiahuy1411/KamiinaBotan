using KamiinaBotan.Core;
using KamiinaBotan.Services;
using KamiinaBotan.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace KamiinaBotan.ViewModels
{
    public class StartMenuViewModel : ViewModelBase
    {
        public string BackgroundPath => "pack://application:,,,/images/StartScreen.jpg";
        private readonly INavigationService _navigation;
        private static bool _isStartVisible = true;
        public bool IsStartVisible
        {
            get => _isStartVisible;
            set => SetProperty(ref _isStartVisible, value);
        }
        
        public RelayCommand TapToStartCommand { get; }
        public RelayCommand PlayCommand { get; }
        public RelayCommand SettingsCommand { get; }
        public RelayCommand ExitCommand { get; }

        public StartMenuViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            TapToStartCommand = new RelayCommand(_ => IsStartVisible = false);
            PlayCommand = new RelayCommand(_ => _navigation.NavigateTo<StageMenuViewModel>());
            SettingsCommand = new RelayCommand(_ => { /* TODO: settings overlay */ });
            ExitCommand = new RelayCommand(_ => Application.Current.Shutdown());
        }
    }
}