using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topics.Data.Entities
{
    public class Post
    {
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public Boolean IsActive { get; set; }
        public string Name { get; set; }
        public int PostID { get; set; }
        public string ShortDescription { get; set; }
        public virtual Topic Topic { get; set; }
        public int TopicID { get; set; }
        public virtual User User { get; set; }
        public int UserID { get; set; }
    }
}
