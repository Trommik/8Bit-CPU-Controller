using System;
using System.Windows;

using CPUController.UI.MVVM;

using JetBrains.Annotations;

using PropertyChanged;

namespace CPUController.UI.ViewModels
{
    public class OpCodeInstructionViewModel : ViewModelBase, IInstructionViewModel
    {
        private readonly OpCodeInstruction _instruction;
        
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
        [DependsOn(nameof(CurrentOpCode))]
        public byte MemorySize => _instruction.NeedsParameter ? (byte) 2 : (byte) 1;
        
        /// <summary>
        /// The current selected <see cref="OpCode"/>.
        /// </summary>
        public OpCode CurrentOpCode
        {
            get => _instruction.OpCode;
            set => _instruction.OpCode = value;
        }

        [UsedImplicitly]
        private void OnCurrentOpCodeChanged()
        {
            OnMemorySizeChanged();
        }
        
        /// <summary>
        /// The visibility of the opcode parameter. 
        /// </summary>
        [DependsOn(nameof(CurrentOpCode))]
        public Visibility ShowParameter => _instruction.NeedsParameter ? Visibility.Visible : Visibility.Hidden;

        /// <summary>
        /// The parameter of the opcode. 
        /// </summary>
        public byte OpCodeParameter
        {
            get => _instruction.Parameter;
            set => _instruction.Parameter = value;
        }

        public OpCodeInstructionViewModel(OpCodeInstruction instruction)
        {
            _instruction = instruction;
        }
    }
}