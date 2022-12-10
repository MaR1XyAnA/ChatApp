using ChatApp.ClassFolder;
using ChatApp.Viwe.WindowsFolder;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatApp.Viwe.PageFolder
{
    public partial class ChatMessagePage : Page
    {
        public static HttpClient httpClient = new HttpClient();
        public static ChatMessageClass chatMessageClass;
        List<ChatMessageClass> chatMessageClasses = new List<ChatMessageClass>();

        public ChatMessagePage()
        {
            InitializeComponent();
            GetMEssage();
        }
        private async void GetMEssage()
        {
            string Link = "http://localhost:11111/api/ChatMessageTables";
            HttpResponseMessage message = await AuthorizationWindows.httpClient.GetAsync(Link);
            var messagecontent = await message.Content.ReadAsStringAsync();
            chatMessageClasses = JsonConvert.DeserializeObject<List<ChatMessageClass>>(messagecontent);

            MessageListBox.ItemsSource = chatMessageClasses.Where(Сookies =>
            Сookies.PNChatRoom == ListChatAndChatMessageWindow.GetChatRoom.PersonalNumberChatRoom);
        }

        private void TextChatTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextChatTextBox.Text.Length == 0)
            {
                ChatTextTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                ChatTextTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private async void GetSendMessage()
        {
            string ServerAddrwssString, JsonString;
            ServerAddrwssString = "http://localhost:11111/chat_message";
            JsonString = "application/json";
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(JsonString));

            var message = new Sender
            {
                PNUsers = AuthorizationWindows.emplyeeClass.PersonalNumberUser,
                PNChatRoom = ListChatAndChatMessageWindow.GetChatRoom.PersonalNumberChatRoom,
                TextMessage = TextChatTextBox.Text
            };
            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(message), Encoding.UTF8, JsonString);
            HttpResponseMessage post = await httpClient.PostAsync(ServerAddrwssString, httpContent);

            if (TextChatTextBox.Text != "")
            {
                if (post.IsSuccessStatusCode)
                {
                    var sweep = await post.Content.ReadAsStringAsync();
                    chatMessageClass = JsonConvert.DeserializeObject<ChatMessageClass>(sweep);
                    TextChatTextBox.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Напиши что нибудь");
            }
            
        }

        public class Sender
        {
            public int PNUsers {get; set;}
            public int PNChatRoom { get; set;}
            public string TextMessage { get; set; }
        }

        private void TextChatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetSendMessage();
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            GetSendMessage();
        }
    }
}
