using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Models;

namespace Topics.Services.Services.Interfaces
{
    public interface ILoginService
    {
        Boolean LoginUser(UserRef user, ValidationMessageList messages);

        void Logoff();
    }
}
