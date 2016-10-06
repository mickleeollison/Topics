using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Enums;

namespace Topics.Core.Models
{
    public class ValidationMessage
    {
        public ValidationMessage(MessageTypes type, string messageText)
        {
            Type = type;
            Text = messageText;
        }

        public string Text { get; set; }
        public MessageTypes Type { get; set; }
    }
}
