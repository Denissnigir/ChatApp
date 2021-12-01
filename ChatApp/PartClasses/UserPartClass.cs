using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Models;

namespace ChatApp.Models
{
    public partial class User
    {
        public string HelloName
        {
            get
            {
                string name = $"Hello {firstName} {secondName}!";
                return name;
            }
        }
    }
}
