using System;
using System.Timers;

using CPUController.DataAccess;

namespace CPUController.Services
{
    public class CpuControllerService : ICpuControllerService
    {
        #region Fields

        private int _refreshRate;
        private Timer? _refreshTimer;

        private string? _endpoint;

        private CpuControllerClient? _cpuClient;

        #endregion

        #region Events

        public event EventHandler<CpuControllerRefreshEventArgs>? Refresh;

        private void OnRefresh(CpuControllerClient client, string endpoint, bool isReachable)
        {
            Refresh?.Invoke(this, new CpuControllerRefreshEventArgs(client, endpoint, isReachable));
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public bool IsConfigValid => _endpoint != null && _refreshRate > 0;

        /// <inheritdoc />
        public ICpuControllerClient CpuClient => _cpuClient ?? throw new InvalidOperationException();

        #endregion

        public void SetEndpoint(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentException($"The {nameof(endpoint)} can't be null or empty!");

            // Dispose the old cpu client
            if (_cpuClient != null)
                _cpuClient.Dispose();

            // Set the endpoint and create a new cpu client
            _endpoint = endpoint;
            _cpuClient = new CpuControllerClient(_endpoint);

            // Start the refresh timer if it exists 
            if (_refreshTimer != null)
                _refreshTimer.Start();
        }

        public void SetRefreshRate(int refreshRate)
        {
            if (refreshRate <= 0)
                throw new ArgumentException($"The {nameof(refreshRate)} must be bigger than 1!");

            // Stop and dispose the old refresh timer 
            if (_refreshTimer != null)
            {
                _refreshTimer.Stop();
                _refreshTimer.Elapsed -= RefreshTimerOnElapsed;
                _refreshTimer.Dispose();
            }

            // Set the refresh rate and create a new refresh timer 
            _refreshRate = refreshRate;
            _refreshTimer = new Timer()
            {
                Interval = _refreshRate,
                AutoReset = true
            };

            _refreshTimer.Elapsed += RefreshTimerOnElapsed;

            // Start the refresh timer if the endpoint is not null or empty
            if (!string.IsNullOrWhiteSpace(_endpoint))
                _refreshTimer.Start();
        }

        private async void RefreshTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (_cpuClient == null || _endpoint == null)
            {
                _refreshTimer?.Stop();

                return;
            }

            var isReachable = false;
            try
            {
                isReachable = await _cpuClient.CheckConnection();
            }
            catch (Exception exception)
            {
                // TODO: Test if exception gets thrown when no connection is established!!!
                Console.WriteLine(exception);
            }

            OnRefresh(_cpuClient, _endpoint, isReachable);
        }
    }
}