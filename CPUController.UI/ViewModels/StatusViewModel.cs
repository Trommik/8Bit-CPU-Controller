using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using CPUController.UI.MVVM;
using CPUController.UI.Windows;

using NLog;

namespace CPUController.UI.ViewModels
{
    public enum ApplicationStatus
    {
        None,
        Ready,
        Error,
        Loading,
        Connecting,
        Disconnecting
    }

    public readonly struct ApplicationStatusMessage
    {
        /// <summary>
        /// The application status for this message.
        /// </summary>
        public ApplicationStatus Status { get; }

        /// <summary>
        /// The application status message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The timestamp when this status was created.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Initializes a new <see cref="ApplicationStatusMessage"/> with the given <paramref name="message" /> and optional <paramref name="status" />.
        /// </summary>
        /// <param name="message">The message which should get reported. (If null or empty the message will get changed to 'Ready...'.</param>
        /// <param name="status">The current <see cref="ApplicationStatus"/>.</param>
        public ApplicationStatusMessage(string message = null, ApplicationStatus status = ApplicationStatus.Ready)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "Ready...";

            Timestamp = DateTime.Now;
            Message = message;
            Status = status;
        }
    }

    public class StatusViewModel : ViewModelBase
    {
        #region Private Fields

        private StatusMessageWindow _statusMessageWindow;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Properties

        /// <summary>
        /// The overall status of the application.
        /// </summary>
        public ApplicationStatus Status { get; private set; }

        /// <summary>
        /// The overall status message of the application.
        /// </summary>
        public string StatusMessage { get; private set; }

        /// <summary>
        /// All status messages which occured in this session.
        /// </summary>
        public ObservableCollection<ApplicationStatusMessage> StatusMessages { get; } = new();

        #endregion

        public StatusViewModel()
        {
            SetStatus(new ApplicationStatusMessage());
        }

        /// <summary>
        /// Changes the current application status to the given <see cref="ApplicationStatusMessage" />.
        /// </summary>
        /// <param name="appStatus"> The application status to which the view model should be set. </param>
        public void SetStatus(ApplicationStatusMessage appStatus)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                StatusMessage = appStatus.Message;
                Status = appStatus.Status;

                StatusMessages.Add(appStatus);
            });

            Logger.Info($"{appStatus.Status.ToString()} - {appStatus.Message}");
        }

        /// <summary>
        /// This command will open a extra window in which all status messages can be viewed. 
        /// </summary>
        public ICommand OpenStatusMessagesWindowCommand => new RelayCommand(OpenStatusMessagesWindow);

        private void OpenStatusMessagesWindow()
        {
            // Lazily create the status message window... 
            _statusMessageWindow ??= new StatusMessageWindow()
            {
                DataContext = this
            };

            // Show the status message window...
            _statusMessageWindow.Show();
        }
    }
}