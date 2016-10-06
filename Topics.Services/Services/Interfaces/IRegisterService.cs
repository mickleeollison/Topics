using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Models;

namespace Topics.Services.Services.Interfaces
{
    public interface IRegisterService
    {
        Boolean RegisterUser(UserRef user, ValidationMessageList messages);
    }
}
