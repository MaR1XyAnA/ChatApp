using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ClassFolder
{
    public partial class ChatMessageClass
    {
        public int PersonalNumberMessage { get; set; }
        public int PNUsers { get; set; }
        public int PNChatRoom { get; set; }
        public string TextMessage { get; set; }
        public DateTime DataTime { get; set; }
        public string TakeUser { get; set; }
    }
}
