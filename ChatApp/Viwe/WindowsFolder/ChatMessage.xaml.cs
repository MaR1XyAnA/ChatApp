using ChatApp.ClassFolder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public partial class ChatMessage : Window
    {
        List<ChatMessageClass> chatMessageClasses = new List<ChatMessageClass>();
        public ChatMessage()
        {
            InitializeComponent();
            Title = MainWi.GetChatRoom.TopicChatRoom;
            GetMEssage();
        }
        private async void GetMEssage()
        {
            string Link = "http://localhost:11111/api/ChatMessageTables";
            HttpResponseMessage message = await AuthorizationWindows.httpClient.GetAsync(Link);
            var messagecontent = await message.Content.ReadAsStringAsync();
            chatMessageClasses = JsonConvert.DeserializeObject<List<ChatMessageClass>>(messagecontent);
            MessageListBox.ItemsSource = chatMessageClasses.Where(Сookies => Сookies.PersonalNumberMessage == MainWi.GetChatRoom.PersonalNumberChatRoom);
        }
    }
}
