using System;

using CPUController.Core.Instructions;
using CPUController.UI.MVVM;

using PropertyChanged;

namespace CPUController.UI.ViewModels
{
    public abstract class InstructionViewModelBase : ViewModelBase
    {
        /// <summary>
        /// Gets fired when the memory size property changed. 
        /// </summary>
        public event EventHandler MemorySizeChanged;

        [SuppressPropertyChangedWarnings]
        protected void OnMemorySizeChanged()
        {
            MemorySizeChanged?.Invoke(this, EventArgs.Empty);
        }
        
        /// <summary>
        /// The <see cref="IInstruction"/> for this <see cref="InstructionViewModelBase"/>.
        /// </summary>
        [SuppressPropertyChangedWarnings]
        public abstract IInstruction Instruction { get; }

        /// <summary>
        /// The current memory address of this instruction. 
        /// </summary>
        [SuppressPropertyChangedWarnings]
        public abstract byte MemoryAddress { get; set; }

        /// <summary>
        /// The current memory size of this instruction. 
        /// </summary>
        [SuppressPropertyChangedWarnings]
        public abstract byte MemorySize { get; }
        
        /// <summary>
        /// The current comment of this instruction. 
        /// </summary>
        [SuppressPropertyChangedWarnings]
        public abstract string Comment { get; set; }
    }
}