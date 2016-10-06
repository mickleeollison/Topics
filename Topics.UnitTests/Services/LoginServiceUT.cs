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
    public class LoginServicesUT
    {
        private bool _bool;
        private HttpContextHelper _httpHelper;
        private LoginService _sut;
        private UserDTO _user;
        private ICollection<UserDTO> _users;
        private Mock<IUserService> _userService;

        public LoginServicesUT()
        {
            _httpHelper = new HttpContextHelper();
            _bool = true;
            _userService = new Mock<IUserService>();
            _sut = new LoginService(_userService.Object);
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
        }

        [Fact]
        public void LoginUser_InvalidPassword()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO() { Salt = "123", PasswordHash = "valid", UserID = 2 };
            UserRef userRef = new UserRef() { UserName = "user2", Password = "invalid" };
            ValidationMessageList vml = new ValidationMessageList();
            _userService.Setup(u => u.GetUserCredentialsByUserID(It.IsAny<int>())).Returns(userCredentials);
            _userService.Setup(u => u.GetUsers()).Returns(_users);
            _sut.LoginUser(userRef, vml);
            Assert.Equal(vml.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault(), ErrorMessages.WRONG_PASSWORD);
        }

        [Fact]
        public void LoginUser_InvalidUserName()
        {
            UserRef userRef = new UserRef() { UserName = "John", Password = "try" };
            ValidationMessageList vml = new ValidationMessageList();
            _userService.Setup(u => u.GetUsers()).Returns(_users);
            _sut.LoginUser(userRef, vml);
            Assert.Equal(vml.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault(), ErrorMessages.NO_USER);
        }

        [Fact]
        public void LoginUser_SuccessfulLogin()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO() { Salt = "123", PasswordHash = "valid123", UserID = 2 };
            UserRef userRef = new UserRef() { UserName = "user2", Password = "valid" };
            ValidationMessageList vml = new ValidationMessageList();
            _userService.Setup(u => u.GetUserCredentialsByUserID(It.IsAny<int>())).Returns(userCredentials);
            _userService.Setup(u => u.GetUsers()).Returns(_users);
            _sut.LoginUser(userRef, vml);
            Assert.Equal(SessionManager.User.UserName, userRef.UserName);
        }

        [Fact]
        public void LoginUser_UserDisabled()
        {
            UserRef userRef = new UserRef() { UserName = "user1", Password = "try" };
            ValidationMessageList vml = new ValidationMessageList();
            _userService.Setup(u => u.GetUsers()).Returns(_users);
            _sut.LoginUser(userRef, vml);
            Assert.Equal(vml.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault(), ErrorMessages.INACTIVE);
        }

        [Fact]
        public void Logoff_Test()
        {
            SessionManager.User = _user;
            _sut.Logoff();
            Assert.Null(SessionManager.User);
        }
    }
}
