
using System;

using CPUController.Services;
using CPUController.UI.MVVM;

using JetBrains.Annotations;

using NLog;

namespace CPUController.UI.ViewModels
{
    public class ControlWordViewModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #region Properties

        /// <summary>
        /// The current control word of the cpu. 
        /// </summary>
        public int ControlWord { get; set; }

        [UsedImplicitly]
        private void OnControlWordChanged()
        {
            // Set the output register address
            OutputRegisterAddress = ControlWord >> 13;
        }
        
        /// <summary>
        /// The current output register address. 
        /// </summary>
        public int OutputRegisterAddress { get; set; }

        #endregion
        
        public ControlWordViewModel(ICpuControllerService cpuControllerService)
        {
            cpuControllerService.Refresh += CpuControllerServiceOnRefresh;
        }

        private async void CpuControllerServiceOnRefresh([CanBeNull]object sender, CpuControllerRefreshEventArgs e)
        {
            if (!e.IsReachable)
                return;
            
            try 
            { 
                var controlWordResponse = await e.Client.GetControlWord();
                ControlWord = controlWordResponse.ControlWord;
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
            }
        }
    }
}