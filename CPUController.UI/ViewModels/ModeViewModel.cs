using System.Windows.Markup;

using CPUController.Services;
using CPUController.UI.MVVM;

using JetBrains.Annotations;

namespace CPUController.UI.ViewModels
{
    public class ModeViewModel : ViewModelBase
    {
        private readonly ICpuControllerService _cpuControllerService;

        #region Properties

        /// <summary>
        /// The current mode of the cpu. 
        /// </summary>
        public CpuMode Mode { get; set; }

        [UsedImplicitly]
        private async void OnModeChanged()
        {
            await _cpuControllerService.CpuClient.SetMode(Mode);
        }

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
        public bool IsReachable { get; set; }

        #endregion

        public ModeViewModel(ICpuControllerService cpuControllerService)
        {
            _cpuControllerService = cpuControllerService;
            _cpuControllerService.Refresh += CpuControllerServiceOnRefresh;
        }

        private async void CpuControllerServiceOnRefresh(object? sender, CpuControllerRefreshEventArgs e)
        {
            var modeResponse = await e.Client.GetMode();
            if (modeResponse.LoadCodeMode)
                Mode = CpuMode.LoadCode;
            else if (modeResponse.ExecuteMode)
                Mode = CpuMode.Execute;
            else
                Mode = CpuMode.None;
            
            IsReachable = e.IsReachable;
        }
    }
}