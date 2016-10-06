using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topics.Data.Entities
{
    public class Topic
    {
        public Boolean IsActive { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public int TopicID { get; set; }
    }
}
