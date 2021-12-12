using System;

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
            // Set the single bits to their value
            HLT = (ControlWord & (1 << 0)) != 0;
            JMP = (ControlWord & (1 << 1)) != 0;
            PCE = (ControlWord & (1 << 2)) != 0;
            ARI = (ControlWord & (1 << 3)) != 0;
            BRI = (ControlWord & (1 << 4)) != 0;
            RDY = (ControlWord & (1 << 5)) != 0;
            ORI = (ControlWord & (1 << 6)) != 0;
            RRI = (ControlWord & (1 << 7)) != 0;
            MAI = (ControlWord & (1 << 8)) != 0;
            FRI = (ControlWord & (1 << 9)) != 0;
            SUB = (ControlWord & (1 << 10)) != 0;
            IRI = (ControlWord & (1 << 11)) != 0;
            CRI = (ControlWord & (1 << 12)) != 0;
            DA0 = (ControlWord & (1 << 13)) != 0;
            DA1 = (ControlWord & (1 << 14)) != 0;
            DA2 = (ControlWord & (1 << 15)) != 0;
        }

        public bool HLT { get; set; }
        public bool JMP { get; set; }
        public bool PCE { get; set; }
        public bool ARI { get; set; }
        public bool BRI { get; set; }
        public bool RDY { get; set; }
        public bool ORI { get; set; }
        public bool RRI { get; set; }
        public bool MAI { get; set; }
        public bool FRI { get; set; }
        public bool SUB { get; set; }
        public bool IRI { get; set; }
        public bool CRI { get; set; }
        public bool DA0 { get; set; }
        public bool DA1 { get; set; }
        public bool DA2 { get; set; }

        /// <summary>
        /// The current output register address. 
        /// </summary>
        public int OutputRegisterAddress { get; set; }

        [UsedImplicitly]
        private void OnOutputRegisterAddressChanged()
        {
            OFF = false;
            PCO = false;
            ARO = false;
            BRO = false;
            ERO = false;
            RRO = false;
            CRO = false;
            EPO = false;
            
            // Set the single bits to their value
            switch (OutputRegisterAddress)
            {
                case 0: OFF = true;
                    break;
                case 1: PCO = true;
                    break;
                case 2: ARO = true;
                    break;
                case 3: BRO = true;
                    break;
                case 4: ERO = true;
                    break;
                case 5: RRO = true;
                    break;
                case 6: CRO = true;
                    break;                
                case 7: EPO = true;
                    break;               
                default:
                    throw new ArgumentOutOfRangeException(nameof(OutputRegisterAddress));
            }
        }

        public bool OFF { get; set; }
        public bool PCO { get; set; }
        public bool ARO { get; set; }
        public bool BRO { get; set; }
        public bool ERO { get; set; }
        public bool RRO { get; set; }
        public bool CRO { get; set; }
        public bool EPO { get; set; }
        
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
            OutputRegisterAddress = ControlWord >> 13;
        }
    }
}