using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Models;
using Topics.Data.Repositories.Interfaces;
using Topics.Services.Services.Interfaces;

namespace Topics.Services.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _UserRepository;

        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public void AddUser(UserDTO User)
        {
            _UserRepository.Add<UserDTO>(User);
        }

        public void AddUserCredentials(UserCredentialsDTO userCredentials)
        {
            _UserRepository.AddUserCredentials<UserCredentialsDTO>(userCredentials);
        }

        public void ChangeUser(int id, UserDTO User)
        {
            _UserRepository.Change<UserDTO>(id, User);
        }

        public UserDTO GetUser(int id)
        {
            return _UserRepository.GetOne<UserDTO>(id);
        }

        public UserDTO GetUserByName(string name)
        {
            return _UserRepository.GetByName<UserDTO>(name);
        }

        public UserCredentialsDTO GetUserCredentialsByUserID(int id)
        {
            return _UserRepository.GetUserCredentialsByUserID<UserCredentialsDTO>(id);
        }

        public ICollection<UserDTO> GetUsers()
        {
            return _UserRepository.GetAll<UserDTO>();
        }

        public void RemoveUser(int id)
        {
            UserDTO user = _UserRepository.GetOne<UserDTO>(id);
            user.IsEnabled = false;
            _UserRepository.Change<UserDTO>(id, user);
        }
    }
}
