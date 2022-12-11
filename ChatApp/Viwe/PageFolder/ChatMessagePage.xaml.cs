using ChatApp.ClassFolder;
using ChatApp.Viwe.WindowsFolder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatApp.Viwe.PageFolder
{
    public partial class ChatMessagePage : Page
    {
        DispatcherTimer dispatcher;
        public static HttpClient httpClient = new HttpClient();
        public static ChatMessageClass chatMessageClass;
        List<ChatMessageClass> chatMessageClasses = new List<ChatMessageClass>();

        public ChatMessagePage()
        {
            InitializeComponent();
            GetMEssage();
            dispatcher = new DispatcherTimer();
            dispatcher.Interval = TimeSpan.FromSeconds(0.1);
            dispatcher.Tick += Dispatcher_Tick;
            dispatcher.Start();

        }

        private void Dispatcher_Tick(object sender, EventArgs e)
        {
            GetMEssage();
        }

        private async void GetMEssage()
        {
            MessageListBox.SelectedIndex = MessageListBox.Items.Count - 1;
            MessageListBox.ScrollIntoView(MessageListBox.SelectedItem);
            string Link = "http://192.168.0.103:11111/api/ChatMessageTables";
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
            ServerAddrwssString = "http://192.168.0.103:11111/chat_message";
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
            if (post.IsSuccessStatusCode)
            {
                var sweep = await post.Content.ReadAsStringAsync();
                chatMessageClass = JsonConvert.DeserializeObject<ChatMessageClass>(sweep);
                TextChatTextBox.Text = "";
            }
        }

        public class Sender
        {
            public int PNUsers { get; set; }
            public int PNChatRoom { get; set; }
            public string TextMessage { get; set; }
        }

        private void TextChatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(TextChatTextBox.Text))
                {

                }
                else
                {
                    GetSendMessage();
                }
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextChatTextBox.Text))
            {

            }
            else
            {
                GetSendMessage();
            }
            
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                MessageListBox.SelectedIndex = MessageListBox.Items.Count - 1;
                MessageListBox.ScrollIntoView(MessageListBox.SelectedItem);
            }
        }
    }
}
