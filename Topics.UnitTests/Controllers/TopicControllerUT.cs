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
    public class TopicControllerUT
    {
        private bool _bool;
        private HttpContextHelper _httpHelper;
        private Mock<IPostService> _postService;
        private TopicController _sut;
        private ICollection<TopicDTO> _Topics;
        private Mock<ITopicService> _TopicService;
        private Mock<IUserService> _userService;
        private RoleDTO role1;
        private RoleDTO role2;
        private ICollection<RoleDTO> roles;
        private TopicDTO Topic1;
        private TopicDTO Topic2;

        public TopicControllerUT()
        {
            _httpHelper = new HttpContextHelper();
            _bool = true;
            _TopicService = new Mock<ITopicService>();
            _userService = new Mock<IUserService>();
            _postService = new Mock<IPostService>();
            _sut = new TopicController(_postService.Object, _TopicService.Object, _userService.Object);
            Topic1 = new TopicDTO() { TopicID = 1, Name = "Topic1" };
            Topic2 = new TopicDTO() { TopicID = 2, Name = "Topic2" };
            _Topics = new Collection<TopicDTO>() { Topic1, Topic2 };
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Create_Test()
        {
            ViewResult result = _sut.Create() as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void CreateTopic_Failure()
        {
            _sut.ModelState.AddModelError("edit", "error");
            ViewResult result = _sut.Create(It.IsAny<TopicVM>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void CreateTopic_Success()
        {
            TopicVM Topic = new TopicVM();
            _TopicService.Setup(u => u.ChangeTopic(It.IsAny<int>(), It.IsAny<TopicDTO>()));
            RedirectToRouteResult result = _sut.Create(Topic) as RedirectToRouteResult;
            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Delete_Test()
        {
            _TopicService.Setup(u => u.GetTopic(It.IsAny<int>())).Returns(Topic1);
            ViewResult result = _sut.Delete(It.IsAny<int>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void DeleteTopic_Test()
        {
            _TopicService.Setup(u => u.RemoveTopic(It.IsAny<int>()));
            RedirectToRouteResult result = _sut.Delete(It.IsAny<int>(), It.IsAny<FormCollection>()) as RedirectToRouteResult;
            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Details_Test()
        {
            PostDTO post = new PostDTO();
            ICollection<PostDTO> posts = new Collection<PostDTO>() { post };
            _postService.Setup(p => p.GetPosts()).Returns(posts);
            _TopicService.Setup(t => t.GetTopic(It.IsAny<int>())).Returns(Topic1);
            ViewResult result = _sut.Details(It.IsAny<int>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void Edit_Test()
        {
            _TopicService.Setup(u => u.GetTopic(It.IsAny<int>())).Returns(Topic1);
            ViewResult result = _sut.Edit(It.IsAny<int>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void EditTopic_Failure()
        {
            _sut.ModelState.AddModelError("edit", "error");
            ViewResult result = _sut.Edit(It.IsAny<TopicVM>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void EditTopic_Success()
        {
            TopicVM Topic = new TopicVM() { TopicID = 3 };
            _TopicService.Setup(u => u.ChangeTopic(It.IsAny<int>(), It.IsAny<TopicDTO>()));
            RedirectToRouteResult result = _sut.Edit(Topic) as RedirectToRouteResult;
            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        public void Index_Test()
        {
            _TopicService.Setup(u => u.GetTopics()).Returns(_Topics);
            ViewResult result = _sut.Index() as ViewResult;
            Assert.Equal(result.ViewName, "");
        }
    }
}
