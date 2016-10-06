using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using Topics.Core.Enums;
using Topics.Core.Models;
using Topics.Services.Services.Interfaces;
using Topics.UnitTests.Helpers;
using Topics.Web.Controllers;
using Topics.Web.Mappers;
using Topics.Web.ViewModels;
using Xunit;

namespace Topics.UnitTests.Controllers
{
    public class UserControllerUT
    {
        private bool _bool;
        private HttpContextHelper _httpHelper;
        private Mock<IRoleService> _roleService;
        private UserController _sut;
        private ICollection<UserDTO> _users;
        private Mock<IUserService> _UserService;
        private RoleDTO role1;
        private RoleDTO role2;
        private ICollection<RoleDTO> roles;
        private UserDTO user1;
        private UserDTO user2;

        public UserControllerUT()
        {
            _httpHelper = new HttpContextHelper();
            _bool = true;
            _UserService = new Mock<IUserService>();
            _roleService = new Mock<IRoleService>();
            _sut = new UserController(_roleService.Object, _UserService.Object);
            role1 = new RoleDTO() { RoleID = 1, Name = "Admin" };
            user1 = new UserDTO() { UserID = 1, IsEnabled = true, UserName = "user1", Role = role1 };
            role2 = new RoleDTO() { RoleID = 2, Name = "User" };
            user2 = new UserDTO() { UserID = 2, IsEnabled = false, UserName = "user2", Role = role2 };
            _users = new Collection<UserDTO>() { user1, user2 };
            AutoMapperConfig.Execute();
            roles = new Collection<RoleDTO>() { role1, role2 };
        }

        [Fact]
        public void Delete_Test()
        {
            _UserService.Setup(u => u.GetUser(It.IsAny<int>())).Returns(user1);
            ViewResult result = _sut.Delete(It.IsAny<int>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void DeleteUser_Test()
        {
            _UserService.Setup(u => u.RemoveUser(It.IsAny<int>()));
            RedirectToRouteResult result = _sut.Delete(It.IsAny<int>(), It.IsAny<FormCollection>()) as RedirectToRouteResult;
            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Edit_Test()
        {
            _UserService.Setup(u => u.GetUser(It.IsAny<int>())).Returns(user1);
            _roleService.Setup(r => r.GetRoles()).Returns(roles);
            ViewResult result = _sut.Edit(It.IsAny<int>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void EditUser_Failure()
        {
            _sut.ModelState.AddModelError("edit", "error");
            ViewResult result = _sut.Edit(It.IsAny<UserVM>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void EditUser_Success()
        {
            UserVM user = new UserVM() { UserID = 3 };
            _UserService.Setup(u => u.ChangeUser(It.IsAny<int>(), It.IsAny<UserDTO>()));
            RedirectToRouteResult result = _sut.Edit(user) as RedirectToRouteResult;
            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        public void Index_Test()
        {
            _UserService.Setup(u => u.GetUsers()).Returns(_users);
            ViewResult result = _sut.Index() as ViewResult;
            Assert.Equal(result.ViewName, "");
        }
    }
}
