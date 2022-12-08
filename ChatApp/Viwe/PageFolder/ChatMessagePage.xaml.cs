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
    /// <summary>
    /// Логика взаимодействия для ChatMessagePage.xaml
    /// </summary>
    public partial class ChatMessagePage : Page
    {
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
            MessageListBox.ItemsSource = chatMessageClasses.Where(Сookies => Сookies.PNChatRoom == ListChatAndChatMessageWindow.GetChatRoom.PersonalNumberChatRoom);
        }
    }
}
