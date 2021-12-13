using System;

using CPUController.DataAccess;

namespace CPUController.Services
{
    public class CpuControllerRefreshEventArgs : EventArgs
    {
        /// <summary>
        /// Returns true if the <see cref="Client"/> is reachable.
        /// </summary>
        public bool IsReachable { get; }

        /// <summary>
        /// The current instance of the <see cref="ICpuControllerClient"/>.
        /// </summary>
        public ICpuControllerClient Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CpuControllerRefreshEventArgs"/> with the given parameters. 
        /// </summary>
        public CpuControllerRefreshEventArgs(ICpuControllerClient client, bool isReachable)
        {
            Client = client;
            IsReachable = isReachable;
        }
    }
}