using System.Windows.Markup;

using CPUController.UI.MVVM;

using JetBrains.Annotations;

namespace CPUController.UI.ViewModels
{
    public class ModeViewModel : ViewModelBase
    {
        /// <summary>
        /// The current mode of the cpu. 
        /// </summary>
        public CpuMode Mode { get; set; }

        [UsedImplicitly]
        private void OnModeChanged()
        {
            
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
        /// True when the cpu is connected else false. 
        /// </summary>
        public bool IsConnected { get; set; }
        
    }
}