using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public partial class ChatMessage
    {
        public int id { get; set; }
        public int senderId { get; set; }
        public int chatroomId { get; set; }
        public DateTime date { get; set; }
        public string message { get; set; }
        public string getMessage { get; set; }
    }
}
