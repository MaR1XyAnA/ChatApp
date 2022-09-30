using ChatApp.Viwe.PageFolder;
using System.Windows;
using System.Windows.Input;

namespace ChatApp.Viwe.WindowsFolder
{
    public partial class AuthorizationWindows : Window
    {
        public AuthorizationWindows()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthorizationPage());
        }

        private void SpaseBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
