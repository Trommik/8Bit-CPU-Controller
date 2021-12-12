using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

using CPUController.DataAccess;
using CPUController.UI.MVVM;
using CPUController.UI.ViewModels;

namespace CPUController.UI
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Test();
        }

        private async void Test()
        {
            var client = new CpuControllerClient("http://192.168.2.50/");
            
            var connection = await client.CheckConnection();
            
            var instr = await client.GetInstructionStatus();
            var mode = await client.GetMode();
            var code = await client.GetCodeStatus();
            var controlWord = await client.GetControlWord();
            
            await client.Reset();
        }
    }
}