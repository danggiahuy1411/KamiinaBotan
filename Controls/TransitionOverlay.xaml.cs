using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace KamiinaBotan.Controls
{
    public partial class TransitionOverlay : UserControl
    {
        private const double BlurRadius = 15;
        private readonly Storyboard _pourIn;
        private readonly Storyboard _pourOut;
        private readonly Storyboard _waveLoop;
        public UIElement? BlurTarget { get; set; }
        public bool IsBusy { get; private set; }
        public TransitionOverlay()
        {
            InitializeComponent();
            _pourIn = (Storyboard)Resources["PourIn"];
            _pourOut = (Storyboard)Resources["PourOut"];
            _waveLoop = (Storyboard)Resources["WaveLoop"];
        }
        public async Task PlayAsync(Action swap)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                await CoverCoreAsync();
                swap();
                await Dispatcher.InvokeAsync(() => { }, DispatcherPriority.Loaded);
                await RevealCoreAsync();
            }
            finally { IsBusy = false; }
        }
        public async Task CoverAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            await CoverCoreAsync();
        }
        private async Task CoverCoreAsync()
        {
            Visibility = Visibility.Visible;
            if (BlurTarget is not null)
                BlurTarget.Effect = new BlurEffect { Radius = 0 };
            AnimateBlur(BlurRadius, beginSec: 0.10, durationSec: 0.70);
            _waveLoop.Begin(this, true);
            await RunAsync(_pourIn);
        }
        private async Task RevealCoreAsync()
        {
            AnimateBlur(0, beginSec: 0.10, durationSec: 0.70);
            await RunAsync(_pourOut);
            _pourIn.Stop(this);
            _pourOut.Stop(this);
            _waveLoop.Stop(this);
            if (BlurTarget is not null) BlurTarget.Effect = null;
            Visibility = Visibility.Collapsed;
        }
        private void AnimateBlur(double to, double beginSec, double durationSec)
        {
            if (BlurTarget?.Effect is not BlurEffect blur) return;
            blur.BeginAnimation(BlurEffect.RadiusProperty, new DoubleAnimation
            {
                To = to,
                BeginTime = TimeSpan.FromSeconds(beginSec),
                Duration = TimeSpan.FromSeconds(durationSec)
            });
        }
        private Task RunAsync(Storyboard sb)
        {
            var tcs = new TaskCompletionSource<bool>();
            void OnCompleted(object? s, EventArgs e)
            {
                sb.Completed -= OnCompleted;
                tcs.SetResult(true);
            }
            sb.Completed += OnCompleted;
            sb.Begin(this, true);
            return tcs.Task;
        }
    }
}