using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using KamiinaBotan.Services;
using KamiinaBotan.ViewModels;

namespace KamiinaBotan;
public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        try
        {
            var services = new ServiceCollection();
            services.AddSingleton<TransitionService>();
            services.AddSingleton<ITransitionService>(sp => sp.GetRequiredService<TransitionService>());
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<StartMenuViewModel>();
            services.AddTransient<StageMenuViewModel>();
            services.AddTransient<TeamMenuViewModel>();
            services.AddTransient<BattlefieldViewModel>();
            services.AddTransient<ResultViewModel>();

            Services = services.BuildServiceProvider();
            var mainWindow = new MainWindow(
                Services.GetRequiredService<MainViewModel>(),
                Services.GetRequiredService<TransitionService>());
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Startup error");
        }
    }
}