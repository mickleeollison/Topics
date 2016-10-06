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
    public class HomeControllerUT
    {
        private bool _bool;
        private HttpContextHelper _httpHelper;
        private HomeController _sut;

        public HomeControllerUT()
        {
            _httpHelper = new HttpContextHelper();
            _bool = true;
            _sut = new HomeController();
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Index_Test()
        {
            ViewResult result = _sut.Index() as ViewResult;
            Assert.Equal(result.ViewName, "");
        }
    }
}
