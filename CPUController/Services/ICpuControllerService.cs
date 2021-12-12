using System;

using CPUController.DataAccess;

namespace CPUController.Services
{
    public interface ICpuControllerService
    {
        /// <summary>
        /// Returns null if the current endpoint and refresh rate is valid.
        /// </summary>
        bool IsConfigValid { get; }

        /// <summary>
        /// Returns the current <see cref="ICpuControllerClient"/>. 
        /// </summary>
        /// <exception cref="InvalidOperationException">Gets thrown when the <see cref="ICpuControllerClient"/> is not initialized. </exception>
        ICpuControllerClient CpuClient { get; }

        event EventHandler<CpuControllerRefreshEventArgs> Refresh;

        void SetEndpoint(string endpoint);
        void SetRefreshRate(int refreshRate);
    }
}