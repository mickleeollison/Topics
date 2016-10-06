using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Models;

namespace Topics.Services.Services.Interfaces
{
    public interface IPostService
    {
        void AddPost(PostDTO Post);

        void ChangePost(int id, PostDTO Post);

        PostDTO GetPost(int id);

        ICollection<PostDTO> GetPosts();

        void RemovePost(int id);
    }
}
