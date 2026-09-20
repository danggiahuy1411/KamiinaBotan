using System;
using System.Collections.Generic;
using System.Text;
using KamiinaBotan.Controls;

namespace KamiinaBotan.Services
{
    public sealed class TransitionService : ITransitionService
    {
        private TransitionOverlay? _overlay;
        public void Attach(TransitionOverlay overlay) => _overlay = overlay;
        public bool IsBusy => _overlay?.IsBusy ?? false;
        public Task PlayAsync(Action swap)
        {
            if (_overlay is null) { swap(); return Task.CompletedTask; }
            return _overlay.PlayAsync(swap);
        }
        public Task CoverAsync() => _overlay?.CoverAsync() ?? Task.CompletedTask;
    }
}