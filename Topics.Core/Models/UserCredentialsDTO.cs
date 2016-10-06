using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topics.Core.Models
{
    public class UserCredentialsDTO
    {
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public UserDTO User { get; set; }
        public int UserID { get; set; }
    }
}
