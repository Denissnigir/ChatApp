using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public partial class ChatroomEmployee
    {
        public int id { get; set; }
        public int chatroomId { get; set; }
        public int employeeId { get; set; }
        public string getName { get; set; }

    }
}
