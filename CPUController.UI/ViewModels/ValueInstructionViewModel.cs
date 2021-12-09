using System;

using CPUController.UI.MVVM;

using PropertyChanged;

namespace CPUController.UI.ViewModels
{
    public class ValueInstructionViewModel : ViewModelBase, IInstructionViewModel
    {
        private readonly ValueInstruction _instruction;
        
        /// <inheritdoc />
        public event EventHandler MemorySizeChanged;
        
        [SuppressPropertyChangedWarnings]
        private void OnMemorySizeChanged()
        {
            MemorySizeChanged?.Invoke(this, EventArgs.Empty);
        }
        
        /// <inheritdoc />
        public IInstruction Instruction => _instruction;

        /// <inheritdoc />
        public byte MemoryAddress
        {
            get => _instruction.Address;
            set => _instruction.Address = value;
        }

        /// <inheritdoc />
        public byte MemorySize => 1;
        
        /// <summary>
        /// The value of the instruction. 
        /// </summary>
        public byte Value
        {
            get => _instruction.Value;
            set => _instruction.Value = value;
        }

        public ValueInstructionViewModel(ValueInstruction instruction)
        {
            _instruction = instruction;
        }
    }
}