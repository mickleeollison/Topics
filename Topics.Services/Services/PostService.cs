using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core;
using Topics.Core.Constants;
using Topics.Core.Models;
using Topics.Core.Utilities;
using Topics.Data.Repositories.Interfaces;
using Topics.Services.Services;
using Topics.Services.Services.Interfaces;

namespace Topics.Services.Services
{
    public class PostService : IPostService
    {
        private IPostRepository _PostRepository;

        public PostService(IPostRepository PostRepository)
        {
            _PostRepository = PostRepository;
        }

        public void AddPost(PostDTO Post)
        {
            Post.UserID = SessionManager.User.UserID;
            Post.ShortDescription = Post.Description.Substring(0, Math.Min(Post.Description.Length, 30)) + "...";
            Post.DateCreated = DateTime.Now;
            Post.IsActive = true;
            _PostRepository.Add<PostDTO>(Post);
        }

        public void ChangePost(int id, PostDTO Post)
        {
            Post.ShortDescription = Post.Description.Substring(0, Math.Min(Post.Description.Length, 30)) + "...";
            _PostRepository.Change<PostDTO>(id, Post);
        }

        public PostDTO GetPost(int id)
        {
            PostDTO post = _PostRepository.GetOne<PostDTO>(id);
            if (SessionManager.User.Role.Name == RolesConstants.ADMIN)
            {
                return post;
            }
            else
            {
                if (post.IsActive)
                {
                    return post;
                }
                else
                {
                    return null;
                }
            }
        }

        public ICollection<PostDTO> GetPosts()
        {
            ICollection<PostDTO> posts = _PostRepository.GetAll<PostDTO>();
            if (SessionManager.User.Role.Name == RolesConstants.ADMIN)
            {
                return posts;
            }
            else
            {
                return posts.Where(p => p.IsActive == true).ToList();
            }
        }

        public void RemovePost(int id)
        {
            PostDTO Post = _PostRepository.GetOne<PostDTO>(id);
            Post.IsActive = false;
            _PostRepository.Change<PostDTO>(id, Post);
        }
    }
}
