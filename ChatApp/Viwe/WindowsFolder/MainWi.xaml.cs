using ChatApp.ClassFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatApp.Viwe.WindowsFolder
{
    public partial class MainWi : Window
    {
        public List<ChatRoom> _ChatRoom = new List<ChatRoom>();

        public MainWi()
        {
            InitializeComponent();
            HelloGrid.DataContext = AuthorizationWindows.emplyeeClass; /*_ChatRoom.ToList();*/
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindows authorizationWindows = new AuthorizationWindows();
            authorizationWindows.Show();
            Close();
        }

        public async void GridLoad()
        {

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
