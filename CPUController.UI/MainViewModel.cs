using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

using CPUController.UI.MVVM;
using CPUController.UI.ViewModels;

namespace CPUController.UI
{
    public class MainViewModel : ViewModelBase
    {
        public bool Connected { get; set; }

        public ControlWordViewModel ControlWordViewModel { get; } = new();

        public OutputRegisterViewModel OutputRegisterViewModel { get; } = new();

        public ConfigViewModel ConfigViewModel { get; } = new();

        public ProgramViewModel ProgramViewModel { get; } = new();

        public ModeViewModel ModeViewModel { get; } = new();
        
        public MainViewModel() { }
    }
}