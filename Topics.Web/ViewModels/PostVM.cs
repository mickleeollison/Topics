using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topics.Web.ViewModels
{
    public class PostVM
    {
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public Boolean IsActive { get; set; }
        public string Name { get; set; }
        public int PostID { get; set; }
        public string ShortDescription { get; set; }
        public TopicVM Topic { get; set; }
        public int TopicID { get; set; }
        public UserVM User { get; set; }
        public int UserID { get; set; }
    }
}
