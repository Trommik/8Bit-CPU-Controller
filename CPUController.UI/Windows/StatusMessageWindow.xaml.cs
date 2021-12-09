using System.ComponentModel;

namespace CPUController.UI.Windows
{
    public partial class StatusMessageWindow
    {
        public StatusMessageWindow()
        {
            InitializeComponent();
        }

        private void StatusMessageWindow_OnClosing(object sender, CancelEventArgs e)
        {
            // Just hide it don't close it 
            Hide();
            e.Cancel = true;
        }
    }
}