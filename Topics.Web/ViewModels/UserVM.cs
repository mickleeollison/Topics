using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Topics.Core.Models;

namespace Topics.Web.ViewModels
{
    public class UserVM
    {
        public Boolean IsEnabled { get; set; }
        public ICollection<PostVM> Posts { get; set; }
        public RoleDTO Role { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
