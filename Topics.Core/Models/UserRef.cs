using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Topics.Core.Models;

namespace Topics.Core.Models
{
    public class UserRef
    {
        public string Password { get; set; }
        public RoleDTO Role { get; set; }
        public int RoleID { get; set; }
        public string UserName { get; set; }
    }
}
