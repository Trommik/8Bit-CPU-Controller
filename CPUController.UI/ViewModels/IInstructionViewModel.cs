using System;

namespace CPUController.UI.ViewModels
{
    public interface IInstructionViewModel
    {
        /// <summary>
        /// Gets fired when the memory size property changed. 
        /// </summary>
        public event EventHandler MemorySizeChanged;

        /// <summary>
        /// The <see cref="IInstruction"/> for this <see cref="IInstructionViewModel"/>.
        /// </summary>
        public IInstruction Instruction { get; }

        /// <summary>
        /// The current memory address of this instruction. 
        /// </summary>
        public byte MemoryAddress { get; set; }

        /// <summary>
        /// The current memory size of this instruction. 
        /// </summary>
        public byte MemorySize { get; }
    }
}