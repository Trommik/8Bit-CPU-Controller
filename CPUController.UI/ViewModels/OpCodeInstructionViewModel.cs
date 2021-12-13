using System;
using System.Windows;

using CPUController.Core;
using CPUController.Core.Instructions;
using CPUController.UI.MVVM;

using JetBrains.Annotations;

using PropertyChanged;

namespace CPUController.UI.ViewModels
{
    public class OpCodeInstructionViewModel : InstructionViewModelBase
    {
        private readonly OpCodeInstruction _instruction;

        /// <inheritdoc />
        public override IInstruction Instruction => _instruction;

        /// <inheritdoc />
        public override byte MemoryAddress
        {
            get => _instruction.Address;
            set => _instruction.Address = value;
        }

        /// <inheritdoc />
        [DependsOn(nameof(OpCode))]
        public override byte MemorySize => _instruction.NeedsParameter ? (byte)2 : (byte)1;

        /// <inheritdoc />
        public override string Comment
        {
            get => _instruction.Comment;
            set => _instruction.Comment = value;
        }

        /// <summary>
        /// The current selected <see cref="Core.OpCode"/> of this instruction.
        /// </summary>
        public OpCode OpCode
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
        /// The current visibility of the opcode parameter. 
        /// </summary>
        [DependsOn(nameof(OpCode))]
        public Visibility ShowParameter => _instruction.NeedsParameter ? Visibility.Visible : Visibility.Hidden;

        /// <summary>
        /// The current parameter of this opcode instruction. 
        /// </summary>
        public byte Parameter
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