using System;
using System.Diagnostics;
using System.Windows.Input;

using JetBrains.Annotations;

namespace CPUController.UI.MVVM
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking
    /// delegates. The default return value for the CanExecute method is 'true'. This class allow
    /// you to accept command parameters in the Execute and CanExecute callback methods.
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class. If canExecute is null it can
        /// always be executed.
        /// </summary>
        /// <param name="execute"> The execution logic. </param>
        /// <param name="canExecute"> The execution status logic. </param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #region ICommand Members

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter"> This parameter will always be ignored. </param>
        /// <returns> True if this command can be executed, otherwise false. </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        [UsedImplicitly]
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter"> This parameter will always be ignored. </param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion ICommand Members
    }
}