using System;
using System.Threading.Tasks;

using CPUController.DataAccess;

namespace CPUController.Services
{
    public interface ICpuControllerService
    {
        /// <summary>
        /// Gets fired when the cpu controller data should be refreshed. 
        /// </summary>
        event EventHandler<CpuControllerRefreshEventArgs> Refresh;

        /// <summary>
        /// Returns null if the current endpoint and refresh rate is valid.
        /// </summary>
        bool IsConfigValid { get; }

        /// <summary>
        /// Returns the current <see cref="ICpuControllerClient"/>. 
        /// </summary>
        /// <exception cref="InvalidOperationException">Gets thrown when the <see cref="ICpuControllerClient"/> is not initialized. </exception>
        ICpuControllerClient CpuClient { get; }

        /// <summary>
        /// Initializes the <see cref="ICpuControllerClient"/> with the given parameters and starts polling. 
        /// </summary>
        /// <param name="endpoint">The endpoint to connect to. </param>
        /// <param name="refreshRate">The polling refresh rate to use. </param>
        Task Initialize(string endpoint, int refreshRate);
    }
}