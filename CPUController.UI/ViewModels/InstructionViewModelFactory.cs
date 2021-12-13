using System;

using CPUController.Core.Instructions;

namespace CPUController.UI.ViewModels
{
    public static class InstructionViewModelFactory
    {
        public static InstructionViewModelBase AsViewModel(this IInstruction instruction)
        {
            switch (instruction)
            {
                case OpCodeInstruction opCodeInstruction:
                    return new OpCodeInstructionViewModel(opCodeInstruction);
                case ValueInstruction valueInstruction:
                    return new ValueInstructionViewModel(valueInstruction);
                default:
                    throw new ArgumentOutOfRangeException(nameof(instruction));
            }
        }
    }
}