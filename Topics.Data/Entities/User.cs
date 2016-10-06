using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topics.Data.Entities
{
    public class User
    {
        public virtual UserCredentials Credentials { get; set; }
        public Boolean IsEnabled { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual Role Role { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
