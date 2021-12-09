using System;

using CPUController.UI.ViewModels;

namespace CPUController.UI.Extensions
{
    internal static class ViewModelExtensions
    {
        internal static IInstructionViewModel AsViewModel(this IInstruction instruction)
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