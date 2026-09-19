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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KamiinaBotan.Views
{
    public partial class StartMenuView : UserControl
    {
        private readonly Storyboard _zoomAndFlashIn;
        private readonly Storyboard _flashOut;
        private bool _isTransitioning;
        public StartMenuView()
        {
            InitializeComponent();
            _zoomAndFlashIn = (Storyboard)Resources["ZoomAndFlashIn"];
            _flashOut = (Storyboard)Resources["FlashOut"];
            _zoomAndFlashIn.Completed += OnFlashInCompleted;
            _flashOut.Completed += OnFlashOutCompleted;
        }
        private void ClickArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isTransitioning) return;
            _isTransitioning = true;
            _zoomAndFlashIn.Begin(this, true);
        }
        private void OnFlashInCompleted(object? sender, EventArgs e)
        {
            (DataContext as StartMenuViewModel)?.TapToStartCommand.Execute(null);
            _flashOut.Begin(this);
        }
        private void OnFlashOutCompleted(object? sender, EventArgs e)
        {
            _zoomAndFlashIn.Stop(this);
            _isTransitioning = false;
        }
    }
}
