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
    public class TopicServicesUT
    {
        private bool _bool;
        private HttpContextHelper _httpHelper;
        private Mock<IPostRepository> _PostRepository;
        private TopicService _sut;
        private Mock<ITopicRepository> _TopicRepository;
        private ICollection<TopicDTO> _Topics;

        public TopicServicesUT()
        {
            _httpHelper = new HttpContextHelper();
            _bool = true;
            _Topics = new Collection<TopicDTO>();
            _TopicRepository = new Mock<ITopicRepository>();
            _PostRepository = new Mock<IPostRepository>();
            _sut = new TopicService(_TopicRepository.Object, _PostRepository.Object);
            TopicDTO Topic1 = new TopicDTO()
            {
                TopicID = 1,
                Name = "Topic1",
                IsActive = false
            };
            TopicDTO Topic2 = new TopicDTO()
            {
                TopicID = 2,
                Name = "Topic2",
                IsActive = true
            };
            _Topics.Add(Topic1);
            _Topics.Add(Topic2);
        }

        [Fact]
        public void AddTopic_Test()
        {
            int TopicCount = _Topics.Count;
            _TopicRepository.Setup(b => b.Add<TopicDTO>(It.IsAny<TopicDTO>()))
                .Callback((TopicDTO newTopic) => _Topics.Add(newTopic));
            _sut.AddTopic(It.IsAny<TopicDTO>());
            Assert.Equal(TopicCount + 1, _Topics.Count);
        }

        [Fact]
        public void ChangeTopic_Test()
        {
            int TopicCount = _Topics.Count;
            _TopicRepository.Setup(b => b.Change<TopicDTO>(It.IsAny<int>(), It.IsAny<TopicDTO>()))
                .Callback((int id, TopicDTO Topic) => _Topics.Remove(_Topics.FirstOrDefault()));
            _sut.ChangeTopic(It.IsAny<int>(), It.IsAny<TopicDTO>());
            Assert.Equal(TopicCount - 1, _Topics.Count);
        }

        [Fact]
        public void GetTopic_ActiveTopic()
        {
            UserDTO user = new UserDTO()
            {
                UserID = 1,
                UserName = "adminUser",
                Role = new RoleDTO { Name = "User" }
            };
            SessionManager.User = user;
            _TopicRepository.Setup(b => b.GetOne<TopicDTO>(It.IsAny<int>())).Returns(_Topics.Where(u => u.TopicID == 2).FirstOrDefault());
            TopicDTO actualTopic = _sut.GetTopic(1);
            Assert.Equal("Topic2", actualTopic.Name);
        }

        [Fact]
        public void GetTopic_AdminUser()
        {
            UserDTO adminUser = new UserDTO()
            {
                UserID = 1,
                UserName = "adminUser",
                Role = new RoleDTO { Name = "Admin" }
            };
            SessionManager.User = adminUser;
            _TopicRepository.Setup(b => b.GetOne<TopicDTO>(It.IsAny<int>())).Returns(_Topics.Where(u => u.TopicID == 1).FirstOrDefault());
            TopicDTO actualTopic = _sut.GetTopic(1);
            Assert.Equal("Topic1", actualTopic.Name);
        }

        [Fact]
        public void GetTopic_InactiveTopic()
        {
            UserDTO user = new UserDTO()
            {
                UserID = 1,
                UserName = "adminUser",
                Role = new RoleDTO { Name = "User" }
            };
            SessionManager.User = user;
            _TopicRepository.Setup(b => b.GetOne<TopicDTO>(It.IsAny<int>())).Returns(_Topics.Where(u => u.TopicID == 1).FirstOrDefault());
            TopicDTO actualTopic = _sut.GetTopic(1);
            Assert.Null(actualTopic);
        }

        [Fact]
        public void GetTopics_AdminUser()
        {
            UserDTO adminUser = new UserDTO()
            {
                UserID = 1,
                UserName = "adminUser",
                Role = new RoleDTO { Name = "Admin" }
            };
            SessionManager.User = adminUser;
            _TopicRepository.Setup(b => b.GetAll<TopicDTO>()).Returns(_Topics);
            ICollection<TopicDTO> actualTopics = _sut.GetTopics();
            Assert.Equal(_Topics.Count, actualTopics.Count);
        }

        [Fact]
        public void GetTopics_NonadminUser()
        {
            UserDTO user = new UserDTO()
            {
                UserID = 1,
                UserName = "User",
                Role = new RoleDTO { Name = "User" }
            };
            SessionManager.User = user;
            _TopicRepository.Setup(b => b.GetAll<TopicDTO>()).Returns(_Topics);
            ICollection<TopicDTO> actualTopics = _sut.GetTopics();
            Assert.Equal(_Topics.Where(t => t.IsActive == true).LongCount(), actualTopics.Count);
        }

        [Fact]
        public void RemoveTopic_Test()
        {
            int Topiccount = _Topics.Count;
            _TopicRepository.Setup(b => b.GetOne<TopicDTO>(It.IsAny<int>())).Returns(_Topics.Where(u => u.TopicID == 1).FirstOrDefault());
            _TopicRepository.Setup(b => b.Change<TopicDTO>(It.IsAny<int>(), It.IsAny<TopicDTO>()))
                .Callback((int id, TopicDTO Topic) => _Topics.Remove(_Topics.FirstOrDefault()));
            _PostRepository.Setup(p => p.GetAll<PostDTO>()).Returns(new Collection<PostDTO>());
            _sut.RemoveTopic(It.IsAny<int>());
            Assert.Equal(Topiccount - 1, _Topics.Count);
        }
    }
}
