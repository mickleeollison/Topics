using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Topics.Core.Models;
using Topics.Data.Repositories.Interfaces;
using Topics.Services.Services;
using Xunit;
using Topics.Core.Models;

namespace Topics.UnitTests.Services
{
    public class UserServicesUT
    {
        private UserService _sut;
        private Mock<IUserRepository> _UserRepository;
        private ICollection<UserDTO> _users;

        public UserServicesUT()
        {
            _users = Collection<UserDTO>();
            _UserRepository = new Mock<IUserRepository>();
            _sut = new UserService(_UserRepository.Object);
            UserDTO user1 = new UserDTO()
            {
            }
        }

        [Fact]
        public void GetUser_Test()
        {
            UserDTO expectedUser = new UserDTO()
            {
                UserID = 1,
                UserName = "User1"
            };
            _UserRepository.Setup(b => b.GetOne<UserDTO>(It.IsAny<int>())).Returns(expectedUser);
            UserDTO actualUser = _sut.GetUser(1);
            Assert.Equal(expectedUser, actualUser);
        }

        [Fact]
        public void GetUsers_Test()
        {
            UserDTO r = null;
            ICollection<UserDTO> rs = new Collection<UserDTO>();
            rs.Add(r);
            _UserRepository.Setup(b => b.GetAll<UserDTO>()).Returns(rs);
            int actual = _sut.GetUsers().Count;
            Assert.Equal(1, actual);
        }
    }
}
