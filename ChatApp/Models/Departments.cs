using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public partial class Departments
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isChecked { get; set; } = true;
    }
}
