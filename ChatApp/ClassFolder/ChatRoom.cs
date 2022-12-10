using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ClassFolder
{
    public partial class ChatRoom
    {
        public int PersonalNumberChatRoom { get; set; }
        public string TopicChatRoom { get; set; }
        public string GetLastMessage { get; set; }
        public string GetLastUser { get; set; }
        public string GetLastTime { get; set; }
    }
}
