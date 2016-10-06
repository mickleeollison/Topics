using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topics.Web.ViewModels
{
    public class TopicVM
    {
        public Boolean IsActive { get; set; }
        public string Name { get; set; }
        public ICollection<PostVM> Posts { get; set; }
        public int TopicID { get; set; }
    }
}
