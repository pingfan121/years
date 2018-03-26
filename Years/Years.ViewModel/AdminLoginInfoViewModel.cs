using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Years.ViewModel
{
    public class AdminLoginInfoViewModel
    {
        public string login_name { get; set; }

        public string login_pass { get; set; }

        public string code { get; set; }

        public bool is_member { get; set; }
    }
}
