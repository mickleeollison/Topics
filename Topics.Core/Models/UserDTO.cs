using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topics.Core.Models
{
    public class UserDTO
    {
        public Boolean IsEnabled { get; set; }
        public ICollection<PostDTO> Posts { get; set; }
        public RoleDTO Role { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
