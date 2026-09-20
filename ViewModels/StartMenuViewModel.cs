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
        private readonly ITransitionService _transition;
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

        public StartMenuViewModel(INavigationService navigation, ITransitionService transition)
        {
            _navigation = navigation;
            _transition = transition;

            TapToStartCommand = new RelayCommand(async _ => await TapToStartAsync());
            PlayCommand = new RelayCommand(_ => _navigation.NavigateTo<StageMenuViewModel>());
            SettingsCommand = new RelayCommand(_ => { /* TODO: settings overlay */ });
            ExitCommand = new RelayCommand(async _ => await ExitAsync());
        }
        private Task TapToStartAsync() => _transition.PlayAsync(() => IsStartVisible = false);
        private async Task ExitAsync()
        {
            await _transition.CoverAsync();
            await Task.Delay(150);
            Application.Current.Shutdown();
        }
    }
}