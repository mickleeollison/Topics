using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topics.Data.Entities
{
    public class UserCredentials
    {
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public virtual User User { get; set; }

        [Key, ForeignKey("User")]
        public int UserID { get; set; }
    }
}
