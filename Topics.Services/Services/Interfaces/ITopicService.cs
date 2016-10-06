using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Models;

namespace Topics.Services.Services.Interfaces
{
    public interface ITopicService
    {
        void AddTopic(TopicDTO Topic);

        void ChangeTopic(int id, TopicDTO Topic);

        TopicDTO GetTopic(int id);

        ICollection<TopicDTO> GetTopics();

        void RemoveTopic(int id);
    }
}
