using System;
using System.Collections.Generic;
using System.Text;

namespace KamiinaBotan.Services
{
    public interface ITransitionService
    {
        bool IsBusy { get; }
        Task PlayAsync(Action swap);
        Task CoverAsync();
    }
}