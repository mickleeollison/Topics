using Moq;
using System.Web.Mvc;
using Topics.Core.Enums;
using Topics.Core.Models;
using Topics.Services.Services.Interfaces;
using Topics.UnitTests.Helpers;
using Topics.Web.Controllers;
using Topics.Web.Mappers;
using Xunit;

namespace Topics.UnitTests.Controllers
{
    public class LoginControllerUT
    {
        private bool _bool;
        private HttpContextHelper _httpHelper;
        private Mock<ILoginService> _loginService;
        private Mock<IRoleService> _roleService;
        private LoginController _sut;

        public LoginControllerUT()
        {
            _httpHelper = new HttpContextHelper();
            _bool = true;
            _loginService = new Mock<ILoginService>();
            _roleService = new Mock<IRoleService>();
            _sut = new LoginController(_loginService.Object, _roleService.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Index_Test()
        {
            _sut.TempData.Add("errorMessage", "login error");
            ViewResult result = _sut.Index() as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void Login_InvalidModelState()
        {
            UserRef model = new UserRef();
            _sut.ModelState.AddModelError("test", "error");
            RedirectToRouteResult actual = _sut.Login(model) as RedirectToRouteResult;

            Assert.Equal(actual.RouteName, "");
        }

        [Fact]
        public void Login_LoginUserReturnsFalse()
        {
            UserRef model = new UserRef();
            ValidationMessage error = new ValidationMessage(MessageTypes.Error, "error");

            _loginService.Setup(l => l.LoginUser(It.IsAny<UserRef>(), It.IsAny<ValidationMessageList>()))
                .Callback((UserRef user, ValidationMessageList messages) => messages.Add(error)).Returns(false);

            RedirectToRouteResult actual = _sut.Login(model) as RedirectToRouteResult;

            Assert.Equal(actual.RouteName.ToString(), "");
            Assert.True(_sut.TempData.ContainsKey("errorMessage"));
        }

        [Fact]
        public void Login_ValidLogin()
        {
            UserRef model = new UserRef();
            ValidationMessage error = new ValidationMessage(MessageTypes.Error, "error");
            _loginService.Setup(l => l.LoginUser(It.IsAny<UserRef>(), It.IsAny<ValidationMessageList>()))
                .Returns(true);
            RedirectToRouteResult actual = _sut.Login(model) as RedirectToRouteResult;
            Assert.Equal(actual.RouteName.ToString(), "");
        }

        [Fact]
        public void Logoff_Test()
        {
            _loginService.Setup(l => l.Logoff());
            RedirectToRouteResult actual = _sut.Logoff() as RedirectToRouteResult;
            Assert.Equal(actual.RouteName.ToString(), "");
        }
    }
}
