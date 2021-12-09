using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

using NLog;

namespace CPUController.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetupExceptionHandling();
        }

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (_, e) => { LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException"); };

            TaskScheduler.UnobservedTaskException += (_, e) =>
            {
                LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
                e.SetObserved();
            };

            DispatcherUnhandledException += (_, e) =>
            {
                LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
                e.Handled = true;
            };
        }

        private void LogUnhandledException(Exception exception, string source)
        {
            var message = $"Unhandled exception ({source})";
            try
            {
                var assemblyName = Assembly.GetExecutingAssembly().GetName();
                message = $"Unhandled exception in {assemblyName.Name} v{assemblyName.Version} ({source})";
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception in LogUnhandledException");
            }
            finally
            {
                _logger.Error(exception, message);
                MessageBox.Show($"{message}\n{exception.Message}", "Unhandled Exception!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}