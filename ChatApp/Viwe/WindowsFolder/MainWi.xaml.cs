using ChatApp.ClassFolder;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace ChatApp.Viwe.WindowsFolder
{
    public partial class MainWi : Window
    {
        public List<ChatRoom> _chatRoom = new List<ChatRoom>();
        public List<ChatRoomUserClass> _ChatRoomEmplooes = new List<ChatRoomUserClass>();
        public static ChatRoom GetChatRoom;
        public MainWi()
        {
            InitializeComponent();
            HelloGrid.DataContext = AuthorizationWindows.emplyeeClass;
            GridLoad();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindows authorizationWindows = new AuthorizationWindows();
            authorizationWindows.Show();
            Close();
        }

        public async void GridLoad()
        {
            HttpResponseMessage chatrooms = await AuthorizationWindows.httpClient.GetAsync("http://localhost:11111/ListChatRooms");
            var roomscontent = await chatrooms.Content.ReadAsStringAsync();

            HttpResponseMessage employee = await AuthorizationWindows.httpClient.GetAsync("http://localhost:11111/api/ChatRoomUserTables");
            var employeecontent = await employee.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ChatRoomUserClass>>(employeecontent)
                .Where(data => data.PNUser == AuthorizationWindows.emplyeeClass.PersonalNumberUser).ToList();

            if(result == null)
            {
                MessageBox.Show("!У ВАС НЕТ НИОДНОГО ЧАТА!");
            }
            else
            {
                var rooms = JsonConvert.DeserializeObject<List<ChatRoom>>(roomscontent).ToList();
                ChatList.ItemsSource = 
                    from r in rooms
                    join res in result on r.PersonalNumberChatRoom equals res.PNChatRoom
                    select r;
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GetChatRoom = ChatList.SelectedItem as ChatRoom;
            ChatMessage chatMessage = new ChatMessage();
            chatMessage.Show();
            this.Close();
        }
    }
}
