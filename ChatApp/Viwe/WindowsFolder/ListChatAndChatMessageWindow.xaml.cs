using ChatApp.ClassFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.ClassFolder;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using ChatApp.Viwe.PageFolder;

namespace ChatApp.Viwe.WindowsFolder
{
    public partial class ListChatAndChatMessageWindow : Window
    {
        public List<ChatRoom> _chatRoom = new List<ChatRoom>();
        public List<ChatRoomUserClass> _ChatRoomEmplooes = new List<ChatRoomUserClass>();
        public static ChatRoom GetChatRoom;
        public ListChatAndChatMessageWindow()
        {
            InitializeComponent();
            GridLoad();
        }

        public async void GridLoad()
        {
            HttpResponseMessage chatrooms = await AuthorizationWindows.httpClient.GetAsync("http://localhost:11111/ListChatRooms");
            var roomscontent = await chatrooms.Content.ReadAsStringAsync();

            HttpResponseMessage employee = await AuthorizationWindows.httpClient.GetAsync("http://localhost:11111/api/ChatRoomUserTables");
            var employeecontent = await employee.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ChatRoomUserClass>>(employeecontent)
                .Where(data => data.PNUser == AuthorizationWindows.emplyeeClass.PersonalNumberUser).ToList();

            if (result == null)
            {
                MessageBox.Show("!У ВАС НЕТ НИОДНОГО ЧАТА!");
            }
            else
            {
                var rooms = JsonConvert.DeserializeObject<List<ChatRoom>>(roomscontent).ToList();
                ListChatListBox.ItemsSource =
                    from r in rooms
                    join res in result on r.PersonalNumberChatRoom equals res.PNChatRoom
                    select r;
            }
        }

        #region WindowsEddit
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindows authorizationWindows = new AuthorizationWindows();
            authorizationWindows.Show();
            this.Close();
        }
        #endregion

        private void ListChatListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetChatRoom = ListChatListBox.SelectedItem as ChatRoom;
            MessageFrame.Navigate(new ChatMessagePage());
        }
    }
}
