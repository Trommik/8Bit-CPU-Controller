using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPUController.UI.MVVM
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking
    /// delegates. The default return value for the CanExecute method is 'true'. This class does not
    /// allow you to accept command parameters in the Execute and CanExecute callback methods.
    /// </summary>
    public class AsyncRelayCommand : ICommand, ICanExecuteCommand
    {
        private long _isExecuting;

        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class. If canExecute is null it can
        /// always be executed.
        /// </summary>
        /// <param name="execute"> The execution logic. </param>
        /// <param name="canExecute"> The execution status logic. </param>
        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (() => true);
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
            return Interlocked.Read(ref _isExecuting) == 0 && _canExecute();
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
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter"> This parameter will always be ignored. </param>
        public async void Execute(object parameter)
        {
            Interlocked.Exchange(ref _isExecuting, 1);

            try
            {
                await _execute();
            }
            finally
            {
                Interlocked.Exchange(ref _isExecuting, 0);
            }
        }

        #endregion ICommand Members
    }
}