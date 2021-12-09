using CPUController.UI.MVVM;

namespace CPUController.UI.ViewModels
{
    public class ControlWordViewModel : ViewModelBase
    {
        public bool HLT { get; set; } = true;
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

        public short ControlWord { get; set; }
    }
}