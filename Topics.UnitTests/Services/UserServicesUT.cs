using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Topics.Core.Models;
using Topics.Data.Repositories.Interfaces;
using Topics.Services.Services;
using Xunit;

namespace Topics.UnitTests.Services
{
    public class UserServicesUT
    {
        private UserService _sut;
        private ICollection<UserCredentialsDTO> _userCredentials;
        private Mock<IUserRepository> _UserRepository;
        private ICollection<UserDTO> _users;

        public UserServicesUT()
        {
            _users = new Collection<UserDTO>();
            _UserRepository = new Mock<IUserRepository>();
            _sut = new UserService(_UserRepository.Object);
            UserDTO user1 = new UserDTO()
            {
                UserID = 1,
                UserName = "user1",
                IsEnabled = true
            };
            UserDTO user2 = new UserDTO()
            {
                UserID = 2,
                UserName = "user2",
                IsEnabled = true
            };
            UserCredentialsDTO UserCred1 = new UserCredentialsDTO() { UserID = 1, PasswordHash = "user1", Salt = "1" };
            UserCredentialsDTO UserCred2 = new UserCredentialsDTO() { UserID = 2, PasswordHash = "user2", Salt = "2" };
            _userCredentials = new List<UserCredentialsDTO>() { UserCred1, UserCred2 };
            _users.Add(user1);
            _users.Add(user2);
        }

        [Fact]
        public void AddUser_Test()
        {
            int usercount = _users.Count;
            _UserRepository.Setup(b => b.Add<UserDTO>(It.IsAny<UserDTO>()))
                .Callback((UserDTO newUser) => _users.Add(newUser));
            _sut.AddUser(It.IsAny<UserDTO>());
            Assert.Equal(usercount + 1, _users.Count);
        }

        [Fact]
        public void AddUserCredentials_Test()
        {
            int userCredentialsCount = _userCredentials.Count;
            _UserRepository.Setup(b => b.AddUserCredentials<UserCredentialsDTO>(It.IsAny<UserCredentialsDTO>()))
                .Callback((UserCredentialsDTO newUserCreds) => _userCredentials.Add(newUserCreds));
            _sut.AddUserCredentials(It.IsAny<UserCredentialsDTO>());
            Assert.Equal(userCredentialsCount + 1, _userCredentials.Count);
        }

        [Fact]
        public void ChangeUser_Test()
        {
            int usercount = _users.Count;
            _UserRepository.Setup(b => b.Change<UserDTO>(It.IsAny<int>(), It.IsAny<UserDTO>()))
                .Callback((int id, UserDTO user) => _users.Remove(_users.FirstOrDefault()));
            _sut.ChangeUser(It.IsAny<int>(), It.IsAny<UserDTO>());
            Assert.Equal(usercount - 1, _users.Count);
        }

        [Fact]
        public void GetCredentialsByUserID_Test()
        {
            _UserRepository.Setup(b => b.GetUserCredentialsByUserID<UserCredentialsDTO>(It.IsAny<int>()))
                .Returns(_userCredentials.Where(c => c.UserID == 1).FirstOrDefault());
            UserCredentialsDTO credentials = _sut.GetUserCredentialsByUserID(1);
            Assert.Equal("user1", credentials.PasswordHash);
        }

        [Fact]
        public void GetUser_Test()
        {
            _UserRepository.Setup(b => b.GetOne<UserDTO>(It.IsAny<int>())).Returns(_users.Where(u => u.UserID == 1).FirstOrDefault());
            UserDTO actualUser = _sut.GetUser(1);
            Assert.Equal("user1", actualUser.UserName);
        }

        [Fact]
        public void GetUserByName_Test()
        {
            _UserRepository.Setup(b => b.GetByName<UserDTO>(It.IsAny<string>())).Returns(_users.Where(u => u.UserName == "user1").FirstOrDefault());
            UserDTO actualUser = _sut.GetUserByName(It.IsAny<string>());
            Assert.Equal("user1", actualUser.UserName);
        }

        [Fact]
        public void GetUsers_Test()
        {
            _UserRepository.Setup(b => b.GetAll<UserDTO>()).Returns(_users);
            ICollection<UserDTO> actualUsers = _sut.GetUsers();
            Assert.Equal(_users.Count, actualUsers.Count);
        }

        [Fact]
        public void RemoveUser_Test()
        {
            int usercount = _users.Count;
            _UserRepository.Setup(b => b.GetOne<UserDTO>(It.IsAny<int>())).Returns(_users.Where(u => u.UserID == 1).FirstOrDefault());
            _UserRepository.Setup(b => b.Change<UserDTO>(It.IsAny<int>(), It.IsAny<UserDTO>()))
                .Callback((int id, UserDTO user) => _users.Remove(_users.FirstOrDefault()));
            _sut.RemoveUser(It.IsAny<int>());
            Assert.Equal(usercount - 1, _users.Count);
        }
    }
}
