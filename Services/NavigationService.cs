using System;
using KamiinaBotan.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace KamiinaBotan.Services;
public class NavigationService : INavigationService
{
    private readonly IServiceProvider _provider;
    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            CurrentViewModelChanged?.Invoke();
        }
    }
    public event Action? CurrentViewModelChanged;
    public NavigationService(IServiceProvider provider)
    {
        _provider = provider;
    }
    public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
    {
        var vm = _provider.GetRequiredService<TViewModel>();
        NavigateTo(vm);
    }
    public void NavigateTo(ViewModelBase viewModel)
    {
        CurrentViewModel = viewModel;
    }
}