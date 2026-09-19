using KamiinaBotan.Services;

namespace KamiinaBotan.ViewModels;
public class MainViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    public ViewModelBase? CurrentViewModel => _navigation.CurrentViewModel;
    public MainViewModel(INavigationService navigation)
    {
        _navigation = navigation;
        _navigation.CurrentViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
        _navigation.NavigateTo<StartMenuViewModel>();
    }
}