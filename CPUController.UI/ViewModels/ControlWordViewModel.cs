
using System;
using System.Diagnostics;

using CPUController.Services;
using CPUController.UI.MVVM;

using JetBrains.Annotations;

namespace CPUController.UI.ViewModels
{
    public class ControlWordViewModel : ViewModelBase
    {
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

        private async void CpuControllerServiceOnRefresh(object? sender, CpuControllerRefreshEventArgs e)
        {
            if (!e.IsReachable)
                return;
            
            var controlWordResponse = await e.Client.GetControlWord();

            ControlWord = controlWordResponse.ControlWord;
        }
    }
}