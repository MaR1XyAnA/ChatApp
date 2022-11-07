using ChatApp.ClassFolder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public List<ChatRoom> _chatRoom = new List<ChatRoom>();
        public List<ChatRoomEmplooe> _ChatRoomEmplooes = new List<ChatRoomEmplooe>();
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
            string Link = "http://localhost:11111/api/ChatRoomTables";

            HttpResponseMessage chatrooms = await AuthorizationWindows.httpClient.GetAsync(Link);
            var roomscontent = await chatrooms.Content.ReadAsStringAsync();

            HttpResponseMessage employee = await AuthorizationWindows.httpClient.GetAsync(Link);
            var employeecontent = await employee.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ChatRoomEmplooe>>(employeecontent)
                .Where(data => data.PersonalNumberCRU == AuthorizationWindows.emplyeeClass.id).ToList();

            if(result == null)
            {
                MessageBox.Show("!У ВАС НЕТ НИОДНОГО ЧАТА!");
            }
            else
            {
                var rooms = JsonConvert.DeserializeObject<List<ChatRoom>>(roomscontent).ToList();
                ChatList.ItemsSource = 
                    from r in rooms
                    join rez in result on r.PersonalNumberChatRoom equals rez.PNChatRoom
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
