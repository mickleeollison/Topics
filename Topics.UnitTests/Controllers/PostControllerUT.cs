using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using Topics.Core.Enums;
using Topics.Core.Models;
using Topics.Core.Utilities;
using Topics.Services.Services.Interfaces;
using Topics.UnitTests.Helpers;
using Topics.Web.Controllers;
using Topics.Web.Mappers;
using Topics.Web.ViewModels;
using Xunit;

namespace Topics.UnitTests.Controllers
{
    public class PostControllerUT
    {
        private bool _bool;
        private HttpContextHelper _httpHelper;
        private ICollection<PostDTO> _Posts;
        private Mock<IPostService> _postService;
        private PostController _sut;
        private ICollection<TopicDTO> _Topics;
        private Mock<ITopicService> _topicService;
        private Mock<IUserService> _userService;
        private PostDTO Post1;
        private PostDTO Post2;
        private RoleDTO role1;
        private RoleDTO role2;
        private TopicDTO Topic1;
        private TopicDTO Topic2;
        private UserDTO user1;
        private UserDTO user2;

        public PostControllerUT()
        {
            role1 = new RoleDTO() { RoleID = 1, Name = "Admin" };
            user1 = new UserDTO() { UserID = 1, IsEnabled = true, UserName = "user1", Role = role1 };
            role2 = new RoleDTO() { RoleID = 2, Name = "User" };
            user2 = new UserDTO() { UserID = 2, IsEnabled = false, UserName = "user2", Role = role2 };
            Post1 = new PostDTO() { PostID = 1, Name = "Post1", UserID = 1 };
            Post2 = new PostDTO() { PostID = 2, Name = "Post2", UserID = 2 };
            _Posts = new Collection<PostDTO>() { Post1, Post2 };
            Topic1 = new TopicDTO() { TopicID = 1, Name = "Topic1" };
            Topic2 = new TopicDTO() { TopicID = 2, Name = "Topic2" };
            _Topics = new Collection<TopicDTO>() { Topic1, Topic2 };
            _httpHelper = new HttpContextHelper();
            _bool = true;
            _topicService = new Mock<ITopicService>();
            _userService = new Mock<IUserService>();
            _postService = new Mock<IPostService>();
            _sut = new PostController(_postService.Object, _topicService.Object, _userService.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Create_Test()
        {
            _postService.Setup(u => u.GetPosts()).Returns(_Posts);
            _topicService.Setup(u => u.GetTopics()).Returns(_Topics);
            ViewResult result = _sut.Create() as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void CreatePost_Failure()
        {
            _sut.ModelState.AddModelError("edit", "error");
            ViewResult result = _sut.Create(It.IsAny<PostVM>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void CreatePost_Success()
        {
            PostVM Post = new PostVM();
            _postService.Setup(u => u.AddPost(It.IsAny<PostDTO>()));
            RedirectToRouteResult result = _sut.Create(Post) as RedirectToRouteResult;
            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Delete_TestFailure()
        {
            SessionManager.User = user2;
            _postService.Setup(u => u.GetPost(It.IsAny<int>())).Returns(Post1);
            RedirectToRouteResult result = _sut.Delete(It.IsAny<int>()) as RedirectToRouteResult;
            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Delete_TestSuccess()
        {
            SessionManager.User = user1;
            _postService.Setup(u => u.GetPost(It.IsAny<int>())).Returns(Post1);
            _postService.Setup(u => u.GetPosts()).Returns(_Posts);
            _topicService.Setup(u => u.GetTopics()).Returns(_Topics);
            ViewResult result = _sut.Delete(It.IsAny<int>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void DeletePost_Test()
        {
            _postService.Setup(u => u.RemovePost(It.IsAny<int>()));
            RedirectToRouteResult result = _sut.Delete(It.IsAny<int>(), It.IsAny<FormCollection>()) as RedirectToRouteResult;
            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Details_Test()
        {
            PostDTO post = null;
            _postService.Setup(p => p.GetPost(It.IsAny<int>())).Returns(post);
            ViewResult result = _sut.Details(It.IsAny<int>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void Edit_TestFailure()
        {
            SessionManager.User = user2;
            _postService.Setup(u => u.GetPost(It.IsAny<int>())).Returns(Post1);
            RedirectToRouteResult result = _sut.Edit(It.IsAny<int>()) as RedirectToRouteResult;
            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Edit_TestSuccess()
        {
            SessionManager.User = user1;
            _postService.Setup(u => u.GetPost(It.IsAny<int>())).Returns(Post1);
            _postService.Setup(u => u.GetPosts()).Returns(_Posts);
            _topicService.Setup(u => u.GetTopics()).Returns(_Topics);
            ViewResult result = _sut.Edit(It.IsAny<int>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void EditPost_Failure()
        {
            _sut.ModelState.AddModelError("edit", "error");
            ViewResult result = _sut.Edit(It.IsAny<PostVM>()) as ViewResult;
            Assert.Equal(result.ViewName, "");
        }

        [Fact]
        public void EditPost_Success()
        {
            PostVM Post = new PostVM() { PostID = 3 };
            _postService.Setup(u => u.ChangePost(It.IsAny<int>(), It.IsAny<PostDTO>()));
            RedirectToRouteResult result = _sut.Edit(Post) as RedirectToRouteResult;
            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        public void Index_Test()
        {
            _sut.TempData.Add("errorMessage", "error!");
            _postService.Setup(u => u.GetPosts()).Returns(_Posts);
            ViewResult result = _sut.Index() as ViewResult;
            Assert.Equal(result.ViewName, "");
        }
    }
}
