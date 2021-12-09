using System;

namespace CPUController.UI.MVVM
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking
    /// delegates. The default return value for the CanExecute method is 'true'. This class does not
    /// allow you to accept command parameters in the Execute and CanExecute callback methods.
    /// </summary>
    public class RelayCommand : RelayCommand<object>
    {
        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute"> The execution logic. </param>
        public RelayCommand(Action execute) : base(p => execute()) { }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute"> The execution logic. </param>
        /// <param name="canExecute"> The execution status logic. </param>
        public RelayCommand(Action execute, Func<bool> canExecute) : base(p => execute(), p => canExecute()) { }
    }
}