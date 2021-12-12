using System;

namespace CPUController.UI.MVVM
{
    public interface ICanExecuteCommand
    {
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        event EventHandler CanExecuteChanged;

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}