using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public class User
    {
        public long id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
        public Nullable<System.DateTime> dateofbirth { get; set; }
        public long roleId { get; set; }
        public string Roles { get; set; }
        public string password { get; set; }        
    }
}
