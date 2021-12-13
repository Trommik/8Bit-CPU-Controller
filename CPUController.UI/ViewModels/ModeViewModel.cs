using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

using CPUController.Core;
using CPUController.Services;
using CPUController.UI.MVVM;

using JetBrains.Annotations;

using NLog;

namespace CPUController.UI.ViewModels
{
    public class ModeViewModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly ICpuControllerService _cpuControllerService;

        #region Properties

        /// <summary>
        /// The current mode of the cpu. 
        /// </summary>
        public CpuMode Mode { get; set; }

        /// <summary>
        /// True when the <see cref="Mode"/> is <see cref="CpuMode.LoadCode"/>.
        /// </summary>
        [DependsOn(nameof(Mode))]
        public bool LoadCode
        {
            get => Mode == CpuMode.LoadCode;
            set => Mode = value ? CpuMode.LoadCode : CpuMode.None;
        }

        /// <summary>
        /// True when the <see cref="Mode"/> is <see cref="CpuMode.Execute"/>.
        /// </summary>
        [DependsOn(nameof(Mode))]
        public bool Execute
        {
            get => Mode == CpuMode.Execute;
            set => Mode = value ? CpuMode.Execute : CpuMode.None;
        }

        /// <summary>
        /// True when the cpus rest server is reachable else false. 
        /// </summary>
        public bool IsCpuReachable { get; private set; }

        #endregion

        public ModeViewModel(ICpuControllerService cpuControllerService)
        {
            _cpuControllerService = cpuControllerService;
            _cpuControllerService.Refresh += CpuControllerServiceOnRefresh;
        }

        private async void CpuControllerServiceOnRefresh([CanBeNull]object sender, CpuControllerRefreshEventArgs e)
        {
            IsCpuReachable = e.IsReachable;
            if (!IsCpuReachable)
                return;

            try 
            { 
                var modeResponse = await e.Client.GetMode();
                if (modeResponse.LoadCodeMode)
                    Mode = CpuMode.LoadCode;
                else if (modeResponse.ExecuteMode)
                    Mode = CpuMode.Execute;
                else
                    Mode = CpuMode.None;
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
            }
        }

        /// <summary>
        /// Command to reset the cpu. 
        /// </summary>
        public ICommand ResetCpuCommand => new AsyncRelayCommand(ResetCpu, CanResetCpu);

        private bool CanResetCpu()
        {
            return IsCpuReachable;
        }
        
        private async Task ResetCpu()
        {
            await _cpuControllerService.CpuClient.Reset();
        }

        /// <summary>
        /// Command to set the cpu into the current mode. 
        /// </summary>
        public ICommand SetModeCommand => new AsyncRelayCommand(SetMode, CanSetMode);

        private bool CanSetMode()
        {
            return IsCpuReachable;
        }
        
        private async Task SetMode()
        {
            await _cpuControllerService.CpuClient.SetMode(Mode);
        }

    }
}