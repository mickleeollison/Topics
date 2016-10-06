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
    public class RegisterControllerUT
    {
        private bool _bool;
        private HttpContextHelper _httpHelper;
        private Mock<IRegisterService> _RegisterService;
        private Mock<IRoleService> _roleService;
        private RegisterController _sut;

        public RegisterControllerUT()
        {
            _httpHelper = new HttpContextHelper();
            _bool = true;
            _RegisterService = new Mock<IRegisterService>();
            _roleService = new Mock<IRoleService>();
            _sut = new RegisterController(_RegisterService.Object, _roleService.Object);
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
        public void Login_RegisterUserReturnsFalse()
        {
            UserRef model = new UserRef();
            ValidationMessage error = new ValidationMessage(MessageTypes.Error, "error");

            _RegisterService.Setup(l => l.RegisterUser(It.IsAny<UserRef>(), It.IsAny<ValidationMessageList>()))
                .Callback((UserRef user, ValidationMessageList messages) => messages.Add(error)).Returns(false);

            RedirectToRouteResult actual = _sut.Register(model) as RedirectToRouteResult;

            Assert.Equal(actual.RouteName.ToString(), "");
            Assert.True(_sut.TempData.ContainsKey("errorMessage"));
        }

        [Fact]
        public void Login_ValidLogin()
        {
            UserRef model = new UserRef();
            _RegisterService.Setup(l => l.RegisterUser(It.IsAny<UserRef>(), It.IsAny<ValidationMessageList>()))
                .Returns(true);
            RedirectToRouteResult actual = _sut.Register(model) as RedirectToRouteResult;
            Assert.Equal(actual.RouteName.ToString(), "");
        }

        [Fact]
        public void Register_InvalidModelState()
        {
            UserRef model = new UserRef();
            _sut.ModelState.AddModelError("test", "error");
            RedirectToRouteResult actual = _sut.Register(model) as RedirectToRouteResult;

            Assert.Equal(actual.RouteName, "");
        }
    }
}
