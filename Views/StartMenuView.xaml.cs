using KamiinaBotan.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KamiinaBotan.Views
{
    public partial class StartMenuView : UserControl
    {
        private readonly Storyboard _pourIn;
        private readonly Storyboard _pourOut;
        private readonly Storyboard _waveLoop;
        private bool _isTransitioning;
        public StartMenuView()
        {
            InitializeComponent();
            _pourIn = (Storyboard)Resources["PourIn"];
            _pourOut = (Storyboard)Resources["PourOut"];
            _waveLoop = (Storyboard)Resources["WaveLoop"];
            _pourIn.Completed += OnPourInCompleted;
            _pourOut.Completed += OnPourOutCompleted;
        }
        private void ClickArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isTransitioning) return;
            _isTransitioning = true;
            Liquid.Visibility = Visibility.Visible;
            Scene.Effect = new BlurEffect { Radius = 0 };
            _waveLoop.Begin(this, true);
            _pourIn.Begin(this, true);
        }
        private void OnPourOutCompleted(object? sender, EventArgs e)
        {
            _pourIn.Stop(this);
            _pourOut.Stop(this);
            _waveLoop.Stop(this);
            Scene.Effect = null;
            Liquid.Visibility = Visibility.Collapsed;
            _isTransitioning = false;
        }
        private void OnPourInCompleted(object? sender, EventArgs e)
        {
            (DataContext as StartMenuViewModel)?.TapToStartCommand.Execute(null);
            _pourOut.Begin(this, true);
        }
    }
}
