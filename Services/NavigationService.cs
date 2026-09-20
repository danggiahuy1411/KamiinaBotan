using System;
using KamiinaBotan.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace KamiinaBotan.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _provider;
        private readonly ITransitionService _transition;
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

        public NavigationService(IServiceProvider provider, ITransitionService transition)
        {
            _provider = provider;
            _transition = transition;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            Navigate(() => _provider.GetRequiredService<TViewModel>());
        }
        public void NavigateTo(ViewModelBase viewModel)
        {
            Navigate(() => viewModel);
        }
        private void Navigate(Func<ViewModelBase> resolve)
        {
            _ = _transition.PlayAsync(() => CurrentViewModel = resolve());
        }
    }
}