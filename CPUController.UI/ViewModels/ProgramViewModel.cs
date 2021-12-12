using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using CPUController.Services;
using CPUController.UI.Extensions;
using CPUController.UI.MVVM;

using Microsoft.Win32;

namespace CPUController.UI.ViewModels
{
    public class ProgramViewModel : ViewModelBase
    {
        private readonly ICpuControllerService _cpuControllerService;

        #region Properties

        /// <summary>
        /// The current selected <see cref="IInstructionViewModel"/> from the <see cref="Instructions"/> collection. 
        /// </summary>
        public IInstructionViewModel SelectedInstruction { get; set; }

        /// <summary>
        /// All current <see cref="IInstructionViewModel"/> instances.
        /// </summary>
        public ObservableCollection<IInstructionViewModel> Instructions { get; set; } = new();

        #endregion

        public ProgramViewModel(ICpuControllerService cpuControllerService)
        {
            _cpuControllerService = cpuControllerService;

            // Initialize the Instructions collection and hookup the memory size changed event
            Instructions.CollectionChanged += (s, e) =>
            {
                if (e.OldItems?.Count > 0)
                    foreach (IInstructionViewModel oldItem in e.OldItems)
                        oldItem.MemorySizeChanged -= Instruction_MemorySizeChanged;

                if (e.NewItems?.Count > 0)
                    foreach (IInstructionViewModel newItem in e.NewItems)
                        newItem.MemorySizeChanged += Instruction_MemorySizeChanged;

                Instruction_MemorySizeChanged(s, EventArgs.Empty);
            };

            Instructions.Add(new OpCodeInstructionViewModel(new OpCodeInstruction() { OpCode = OpCode.HLT }));
        }

        private void Instruction_MemorySizeChanged(object sender, EventArgs e)
        {
            // Update the memory address of each instruction when the collection changes
            for (byte i = 0; i < Instructions.Count; i++)
            {
                var currInstruction = Instructions[i];

                if (i == 0)
                {
                    currInstruction.MemoryAddress = 0;
                }
                else
                {
                    var prevInstruction = Instructions[i - 1];
                    currInstruction.MemoryAddress = (byte)(prevInstruction.MemoryAddress + prevInstruction.MemorySize);
                }
            }
        }

        #region Commands

        public ICommand SaveCommand => new RelayCommand(Save);

        private void Save()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Title = "Save program to ...",
                RestoreDirectory = true,
                CheckPathExists = true,
                AddExtension = true,
                Filter = "CPU code (*.cpc)|*.cpc|All files (*.*)|*.*",
                DefaultExt = "*.cpc"
            };

            // Check if aborted
            if (saveFileDialog.ShowDialog() != true)
                return;

            // Create file content
            var builder = new StringBuilder();
            foreach (var instructionViewModel in Instructions)
            {
                builder.AppendLine(instructionViewModel.Instruction.ToString());
            }

            // Save as file
            File.WriteAllText(saveFileDialog.FileName, builder.ToString());
        }

        public ICommand LoadCommand => new RelayCommand(Load);

        private void Load()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Title = "Select program ...",
                RestoreDirectory = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "CPU code (*.cpc)|*.cpc|All files (*.*)|*.*",
            };

            // Check if aborted
            if (openFileDialog.ShowDialog() != true)
                return;

            // Read file
            string[] lines = File.ReadAllLines(openFileDialog.FileName);

            // Clear all other instructions
            Instructions.Clear();

            // Parse all lines
            foreach (string line in lines)
            {
                // TODO: Try catch!!!
                var instruction = InstructionParser.Parse(line);
                Instructions.Add(instruction.AsViewModel());
            }
        }
        
        /// <summary>
        /// Command to clear the <see cref="Instructions"/> collection.
        /// </summary>
        public ICommand ClearInstructionsCommand => new RelayCommand(ClearInstructions, CanClearInstructions);

        private bool CanClearInstructions() => Instructions.Any();

        private void ClearInstructions()
        {
            Instructions.Clear();
        }
        
        /// <summary>
        /// Command to delete the current <see cref="SelectedInstruction"/> from the <see cref="Instructions"/> collection.
        /// </summary>
        public ICommand DeleteInstructionCommand => new RelayCommand(DeleteInstruction, CanDeleteInstruction);

        private bool CanDeleteInstruction() => SelectedInstruction != null;

        private void DeleteInstruction()
        {
            Instructions.Remove(SelectedInstruction);
        }

        /// <summary>
        /// Command to add a new <see cref="OpCodeInstruction"/> to the <see cref="Instructions"/> collection.
        /// </summary>
        public ICommand AddInstructionCommand => new RelayCommand(AddInstruction, CanAddInstruction);

        private bool CanAddInstruction()
        {
            if (!Instructions.Any())
                return true;

            var lastInstruction = Instructions.Last();

            return lastInstruction.MemoryAddress + lastInstruction.MemorySize < 255;
        }

        private void AddInstruction()
        {
            var newInstruction = new OpCodeInstruction()
            {
                Address = (byte)Instructions.Count
            };

            Instructions.Add(newInstruction.AsViewModel());
        }

        /// <summary>
        /// Command to add a new <see cref="ValueInstruction"/> to the <see cref="Instructions"/> collection.
        /// </summary>
        public ICommand AddValueCommand => new RelayCommand(AddValue, CanAddValue);

        private bool CanAddValue()
        {
            if (!Instructions.Any())
                return true;

            var lastInstruction = Instructions.Last();

            return lastInstruction.MemoryAddress + lastInstruction.MemorySize < 255;
        }

        private void AddValue()
        {
            var newInstruction = new ValueInstruction()
            {
                Address = (byte)Instructions.Count
            };

            Instructions.Add(newInstruction.AsViewModel());
        }

        /// <summary>
        /// Command to upload the current <see cref="Instructions"/> to the cpu. 
        /// </summary>
        public ICommand UploadToCpuCommand => new AsyncRelayCommand(UploadToCpu, CanUploadToCpu);

        private bool CanUploadToCpu() => Instructions.Any();

        private async Task UploadToCpu()
        {
            var binaries = Instructions.SelectMany(vm => vm.Instruction.ToBinary());

            await _cpuControllerService.CpuClient.SetCode(binaries);
        }

        #endregion
    }
}