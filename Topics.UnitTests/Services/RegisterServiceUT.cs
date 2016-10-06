using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Topics.Core.Constants;
using Topics.Core.Enums;
using Topics.Core.Models;
using Topics.Core.Utilities;
using Topics.Services.Services;
using Topics.Services.Services.Interfaces;
using Topics.UnitTests.Helpers;
using Xunit;

namespace UnitTest.Services
{
    public class RegisterServicesUT
    {
        private RegisterService _sut;
        private UserDTO _user;
        private ICollection<UserCredentialsDTO> _userCredentials;
        private ICollection<UserDTO> _users;
        private Mock<IUserService> _userService;

        public RegisterServicesUT()
        {
            _userService = new Mock<IUserService>();
            _sut = new RegisterService(_userService.Object);
            UserDTO user1 = new UserDTO()
            {
                UserName = "user1",
                UserID = 1,
                IsEnabled = false
            };

            UserDTO user2 = new UserDTO()
            {
                UserName = "user2",
                UserID = 2,
                IsEnabled = true
            };

            _users = new Collection<UserDTO>();
            _users.Add(user1);
            _users.Add(user2);
            _userCredentials = new Collection<UserCredentialsDTO>();
        }

        [Fact]
        public void RegisterUser_NameInUse()
        {
            UserRef userRef = new UserRef() { UserName = "user1", Password = "try" };
            ValidationMessageList vml = new ValidationMessageList();
            _userService.Setup(u => u.GetUsers()).Returns(_users);
            _sut.RegisterUser(userRef, vml);
            Assert.Equal(vml.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault(), ErrorMessages.USERNAME_IN_USE);
        }

        [Fact]
        public void RegisterUser_Successful()
        {
            UserRef userRef = new UserRef() { UserName = "user3", Password = "try" };
            ValidationMessageList vml = new ValidationMessageList();
            _userService.Setup(u => u.GetUsers()).Returns(_users);
            _userService.Setup(u => u.AddUser(It.IsAny<UserDTO>()));
            _userService.Setup(u => u.AddUserCredentials(It.IsAny<UserCredentialsDTO>()));
            _userService.Setup(u => u.GetUserByName(It.IsAny<string>())).Returns(new UserDTO());
            bool actual = _sut.RegisterUser(userRef, vml);
            Assert.Equal(true, actual);
        }
    }
}
