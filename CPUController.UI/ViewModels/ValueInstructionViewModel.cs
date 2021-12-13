using System;

using CPUController.Core.Instructions;
using CPUController.UI.MVVM;

using PropertyChanged;

namespace CPUController.UI.ViewModels
{
    public class ValueInstructionViewModel : InstructionViewModelBase
    {
        private readonly ValueInstruction _instruction;

        /// <inheritdoc />
        public override IInstruction Instruction => _instruction;

        /// <inheritdoc />
        public override byte MemoryAddress
        {
            get => _instruction.Address;
            set => _instruction.Address = value;
        }

        /// <inheritdoc />
        public override byte MemorySize => 1;

        /// <inheritdoc />
        public override string Comment
        {
            get => _instruction.Comment;
            set => _instruction.Comment = value;
        }

        /// <summary>
        /// The current value of this instruction. 
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