using CPUController.UI.MVVM;

namespace CPUController.UI.ViewModels
{
    public class OutputRegisterViewModel : ViewModelBase
    {
        public bool PCO { get; set; }
        public bool ARO { get; set; }
        public bool BRO { get; set; }
        public bool ERO { get; set; }
        public bool RRO { get; set; }
        public bool CRO { get; set; }
        public bool EPO { get; set; }
    }
}