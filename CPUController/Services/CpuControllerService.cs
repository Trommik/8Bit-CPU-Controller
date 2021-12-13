using System;
using System.Threading;
using System.Threading.Tasks;

using CPUController.DataAccess;

namespace CPUController.Services
{
    public class CpuControllerService : ICpuControllerService
    {
        #region Fields

        private int _refreshRate;
        private string? _endpoint;

        private CpuControllerClient? _cpuClient;

        private Task? _pollingTask; 
        private CancellationTokenSource? _cancellationTokenSource;
        
        #endregion

        #region Events

        /// <inheritdoc />
        public event EventHandler<CpuControllerRefreshEventArgs>? Refresh;

        private void OnRefresh(CpuControllerClient client, bool isReachable)
        {
            Refresh?.Invoke(this, new CpuControllerRefreshEventArgs(client, isReachable));
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public bool IsConfigValid => _endpoint != null && _refreshRate > 0;

        /// <inheritdoc />
        public ICpuControllerClient CpuClient => _cpuClient ?? throw new InvalidOperationException();

        #endregion

        /// <inheritdoc />
        public async Task Initialize(string endpoint, int refreshRate)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentException($"The {nameof(endpoint)} can't be null or empty!");
            
            if (refreshRate <= 0)
                throw new ArgumentException($"The {nameof(refreshRate)} must be bigger than 1!");

            // Clean up the old resources
            if (_pollingTask != null && _cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                await _pollingTask;
                _cancellationTokenSource.Dispose();
            }
            
            if (_cpuClient != null)
            {
                _cpuClient.Dispose();
            }

            // Set the endpoint refresh rate 
            _endpoint = endpoint;
            _refreshRate = refreshRate;
            
            // Create a new cpu client
            _cpuClient = new CpuControllerClient(_endpoint);

            // Start a new polling task
            _pollingTask = StartPollingTask();
        }

        private Task StartPollingTask()
        {
            if (_cpuClient == null)
                throw new InvalidOperationException("Client not initialized!");

            // Create a new cancellation token 
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            
            return Task.Run(async() =>
            {
                while (true)
                {
                    // Poll ESP8266 API
                    var isReachable = false;
                    try
                    {
                        isReachable = await _cpuClient.CheckConnection();
                    }
                    catch
                    {
                        /* ignored */
                    }

                    // Fire the refresh event
                    OnRefresh(_cpuClient, isReachable);
                    
                    // Wait till the refresh rate is elapsed
                    Thread.Sleep(_refreshRate);
                    
                    // Check if cancellation is requested
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            }, cancellationToken);
        }
    }
}