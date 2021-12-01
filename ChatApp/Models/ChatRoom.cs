using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public partial class ChatRoom
    {
        public int id { get; set; }
        public string topic { get; set; }
        public string lastMessage { get; set; } = null;
    }
}
