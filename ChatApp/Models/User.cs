using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public partial class User
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public int departmentId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string getName { get; set; }

    }
}
