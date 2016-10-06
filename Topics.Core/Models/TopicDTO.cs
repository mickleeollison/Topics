using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topics.Core.Models
{
    public class TopicDTO
    {
        public Boolean IsActive { get; set; }
        public string Name { get; set; }
        public ICollection<PostDTO> Posts { get; set; }
        public int TopicID { get; set; }
    }
}
