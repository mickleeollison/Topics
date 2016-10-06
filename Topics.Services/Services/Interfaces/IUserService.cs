using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Models;

namespace Topics.Services.Services.Interfaces
{
    public interface IUserService
    {
        void AddUser(UserDTO User);

        void AddUserCredentials(UserCredentialsDTO userCredentials);

        void ChangeUser(int id, UserDTO User);

        UserDTO GetUser(int id);

        UserDTO GetUserByName(string name);

        UserCredentialsDTO GetUserCredentialsByUserID(int id);

        ICollection<UserDTO> GetUsers();

        void RemoveUser(int id);
    }
}
