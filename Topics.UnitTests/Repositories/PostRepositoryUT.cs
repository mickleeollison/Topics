using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using Topics.Core.Models;
using Topics.Data.DAL.Interfaces;
using Topics.Data.Entities;
using Topics.Data.Mappers;
using Topics.Data.Repositories;
using Topics.UnitTests.Helpers;
using Xunit;

namespace Topics.UnitTests.Repositories
{
    public class PostRepositoryUT
    {
        private Mock<ITopicsContext> _db;
        private DbSetHelper _helper;
        private List<Post> _PostList;
        private Mock<DbSet<Post>> _PostSet;
        private PostRepository _sut;

        public PostRepositoryUT()
        {
            _helper = new DbSetHelper();
            _db = new Mock<ITopicsContext>();
            _sut = new PostRepository(_db.Object);

            Post Post1 = new Post() { PostID = 1, Name = "Post1" };
            Post Post2 = new Post() { PostID = 2, Name = "Post2" };
            _PostList = new List<Post>() { Post1, Post2 };

            _PostSet = _helper.GetDbSet(_PostList);
            _db.Setup(c => c.Posts).Returns(_PostSet.Object);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void AddPost_Test()
        {
            _db.Setup(c => c.Posts.Add(It.IsAny<Post>()))
                .Callback((Post Post) => _PostList.Add(Post));
            _sut.Add<PostDTO>(new PostDTO());
            Assert.Equal(3, _PostList.Count);
        }

        [Fact]
        public void GetAll_PostDTO_Valid()
        {
            ICollection<PostDTO> actual = _sut.GetAll<PostDTO>();
            Assert.Equal(_PostList.Count, actual.Count);
        }

        [Fact]
        public void GetPostById_Test()
        {
            PostDTO actual = _sut.GetOne<PostDTO>(1);
            Assert.Equal("Post1", actual.Name);
        }
    }
}
