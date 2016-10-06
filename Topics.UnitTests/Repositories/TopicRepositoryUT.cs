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
    public class TopicRepositoryUT
    {
        private Mock<ITopicsContext> _db;
        private DbSetHelper _helper;
        private TopicRepository _sut;
        private List<Topic> _TopicList;
        private Mock<DbSet<Topic>> _TopicSet;

        public TopicRepositoryUT()
        {
            _helper = new DbSetHelper();
            _db = new Mock<ITopicsContext>();
            _sut = new TopicRepository(_db.Object);

            Topic Topic1 = new Topic() { TopicID = 1, Name = "Topic1" };
            Topic Topic2 = new Topic() { TopicID = 2, Name = "Topic2" };
            _TopicList = new List<Topic>() { Topic1, Topic2 };

            _TopicSet = _helper.GetDbSet(_TopicList);
            _db.Setup(c => c.Topics).Returns(_TopicSet.Object);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void AddTopic_Test()
        {
            _db.Setup(c => c.Topics.Add(It.IsAny<Topic>()))
                .Callback((Topic Topic) => _TopicList.Add(Topic));
            _sut.Add<TopicDTO>(new TopicDTO());
            Assert.Equal(3, _TopicList.Count);
        }

        [Fact]
        public void GetAll_TopicDTO_Valid()
        {
            ICollection<TopicDTO> actual = _sut.GetAll<TopicDTO>();
            Assert.Equal(_TopicList.Count, actual.Count);
        }

        [Fact]
        public void GetTopicById_Test()
        {
            TopicDTO actual = _sut.GetOne<TopicDTO>(1);
            Assert.Equal("Topic1", actual.Name);
        }
    }
}
