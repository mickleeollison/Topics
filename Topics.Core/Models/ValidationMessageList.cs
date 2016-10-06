using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topics.Core.Models
{
    public class ValidationMessageList : List<ValidationMessage>
    {
        public bool HasError
        {
            get
            {
                return this.Where(x => x.Type == Enums.MessageTypes.Error).Any();
            }
        }
    }
}
