using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Constants;
using Topics.Core.Models;
using Topics.Core.Utilities;
using Topics.Data.Repositories.Interfaces;
using Topics.Services.Services.Interfaces;

namespace Topics.Services.Services
{
    public class TopicService : ITopicService
    {
        private IPostRepository _postRepository;
        private ITopicRepository _TopicRepository;

        public TopicService(ITopicRepository TopicRepository, IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _TopicRepository = TopicRepository;
        }

        public void AddTopic(TopicDTO Topic)
        {
            _TopicRepository.Add<TopicDTO>(Topic);
        }

        public void ChangeTopic(int id, TopicDTO Topic)
        {
            _TopicRepository.Change<TopicDTO>(id, Topic);
        }

        public TopicDTO GetTopic(int id)
        {
            TopicDTO topic = _TopicRepository.GetOne<TopicDTO>(id);
            if (SessionManager.User.Role.Name == RolesConstants.ADMIN)
            {
                return topic;
            }
            else
            {
                if (topic.IsActive)
                {
                    return topic;
                }
                else
                {
                    return null;
                }
            }
        }

        public ICollection<TopicDTO> GetTopics()
        {
            ICollection<TopicDTO> topics = _TopicRepository.GetAll<TopicDTO>();
            if (SessionManager.User.Role.Name == RolesConstants.ADMIN)
            {
                return topics;
            }
            else
            {
                return topics.Where(t => t.IsActive == true).ToList();
            }
        }

        public void RemoveTopic(int id)
        {
            TopicDTO Topic = _TopicRepository.GetOne<TopicDTO>(id);
            Topic.IsActive = false;
            _TopicRepository.Change<TopicDTO>(id, Topic);
            ICollection<PostDTO> posts = _postRepository.GetAll<PostDTO>();
            foreach (PostDTO post in posts)
            {
                if (post.TopicID == id)
                {
                    post.IsActive = false;
                    _postRepository.Change<PostDTO>(id, post);
                }
            }
        }
    }
}
