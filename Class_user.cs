using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SHOP
{
    public class user
    {
        public string login {  get; set; }
        public string pass  { get; set; }
        public int role { get; set; }

        public user(string l, string p, int r) {login = l; pass = p; role = r;}

    }
}
