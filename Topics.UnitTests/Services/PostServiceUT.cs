using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Topics.Core.Models;
using Topics.Core.Utilities;
using Topics.Data.Repositories.Interfaces;
using Topics.Services.Services;
using Topics.UnitTests.Helpers;
using Xunit;

namespace Topics.UnitTests.Services
{
    public class PostServicesUT
    {
        private bool _bool;
        private HttpContextHelper _httpHelper;
        private Mock<IPostRepository> _PostRepository;
        private ICollection<PostDTO> _Posts;
        private PostService _sut;

        public PostServicesUT()
        {
            _httpHelper = new HttpContextHelper();
            _bool = true;
            _Posts = new Collection<PostDTO>();
            _PostRepository = new Mock<IPostRepository>();
            _sut = new PostService(_PostRepository.Object);
            PostDTO Post1 = new PostDTO()
            {
                PostID = 1,
                Name = "Post1",
                IsActive = false
            };
            PostDTO Post2 = new PostDTO()
            {
                PostID = 2,
                Name = "Post2",
                IsActive = true
            };
            _Posts.Add(Post1);
            _Posts.Add(Post2);
        }

        [Fact]
        public void AddPost_Test()
        {
            UserDTO user = new UserDTO()
            {
                UserID = 1,
                UserName = "adminUser",
                Role = new RoleDTO { Name = "User" }
            };
            SessionManager.User = user;
            PostDTO newPost = new PostDTO()
            {
                Description = "New post added"
            };
            int PostCount = _Posts.Count;
            _PostRepository.Setup(b => b.Add<PostDTO>(It.IsAny<PostDTO>()))
                .Callback((PostDTO post) => _Posts.Add(newPost));
            _sut.AddPost(newPost);
            Assert.Equal(PostCount + 1, _Posts.Count);
        }

        [Fact]
        public void ChangePost_Test()
        {
            PostDTO newPost = new PostDTO()
            {
                Description = "Existing post Changed"
            };
            int PostCount = _Posts.Count;
            _PostRepository.Setup(b => b.Change<PostDTO>(It.IsAny<int>(), It.IsAny<PostDTO>()))
                .Callback((int id, PostDTO Post) => _Posts.Remove(_Posts.FirstOrDefault()));
            _sut.ChangePost(It.IsAny<int>(), newPost);
            Assert.Equal(PostCount - 1, _Posts.Count);
        }

        [Fact]
        public void GetPost_ActivePost()
        {
            UserDTO user = new UserDTO()
            {
                UserID = 1,
                UserName = "adminUser",
                Role = new RoleDTO { Name = "User" }
            };
            SessionManager.User = user;
            _PostRepository.Setup(b => b.GetOne<PostDTO>(It.IsAny<int>())).Returns(_Posts.Where(u => u.PostID == 2).FirstOrDefault());
            PostDTO actualPost = _sut.GetPost(1);
            Assert.Equal("Post2", actualPost.Name);
        }

        [Fact]
        public void GetPost_AdminUser()
        {
            UserDTO adminUser = new UserDTO()
            {
                UserID = 1,
                UserName = "adminUser",
                Role = new RoleDTO { Name = "Admin" }
            };
            SessionManager.User = adminUser;
            _PostRepository.Setup(b => b.GetOne<PostDTO>(It.IsAny<int>())).Returns(_Posts.Where(u => u.PostID == 1).FirstOrDefault());
            PostDTO actualPost = _sut.GetPost(1);
            Assert.Equal("Post1", actualPost.Name);
        }

        [Fact]
        public void GetPost_InactivePost()
        {
            UserDTO user = new UserDTO()
            {
                UserID = 1,
                UserName = "adminUser",
                Role = new RoleDTO { Name = "User" }
            };
            SessionManager.User = user;
            _PostRepository.Setup(b => b.GetOne<PostDTO>(It.IsAny<int>())).Returns(_Posts.Where(u => u.PostID == 1).FirstOrDefault());
            PostDTO actualPost = _sut.GetPost(1);
            Assert.Null(actualPost);
        }

        [Fact]
        public void GetPosts_AdminUser()
        {
            UserDTO adminUser = new UserDTO()
            {
                UserID = 1,
                UserName = "adminUser",
                Role = new RoleDTO { Name = "Admin" }
            };
            SessionManager.User = adminUser;
            _PostRepository.Setup(b => b.GetAll<PostDTO>()).Returns(_Posts);
            ICollection<PostDTO> actualPosts = _sut.GetPosts();
            Assert.Equal(_Posts.Count, actualPosts.Count);
        }

        [Fact]
        public void GetPosts_NonadminUser()
        {
            UserDTO user = new UserDTO()
            {
                UserID = 1,
                UserName = "User",
                Role = new RoleDTO { Name = "User" }
            };
            SessionManager.User = user;
            _PostRepository.Setup(b => b.GetAll<PostDTO>()).Returns(_Posts);
            ICollection<PostDTO> actualPosts = _sut.GetPosts();
            Assert.Equal(_Posts.Where(t => t.IsActive == true).LongCount(), actualPosts.Count);
        }

        [Fact]
        public void RemovePost_Test()
        {
            int Postcount = _Posts.Count;
            _PostRepository.Setup(b => b.GetOne<PostDTO>(It.IsAny<int>())).Returns(_Posts.Where(u => u.PostID == 1).FirstOrDefault());
            _PostRepository.Setup(b => b.Change<PostDTO>(It.IsAny<int>(), It.IsAny<PostDTO>()))
                .Callback((int id, PostDTO Post) => _Posts.Remove(_Posts.FirstOrDefault()));
            _PostRepository.Setup(p => p.GetAll<PostDTO>()).Returns(new Collection<PostDTO>());
            _sut.RemovePost(It.IsAny<int>());
            Assert.Equal(Postcount - 1, _Posts.Count);
        }
    }
}
