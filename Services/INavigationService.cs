using KamiinaBotan.ViewModels;

namespace KamiinaBotan.Services;
public interface INavigationService
{
    ViewModelBase? CurrentViewModel { get; }
    event Action? CurrentViewModelChanged;
    void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
    void NavigateTo(ViewModelBase viewModel);
}