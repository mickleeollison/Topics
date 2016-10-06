using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Models;
using Topics.Data.DAL.Interfaces;
using Topics.Data.Entities;
using Topics.Data.Repositories.Interfaces;

namespace Topics.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private ITopicsContext _db;
        private IUnitOfWork _uow;

        public PostRepository(ITopicsContext topicsContext)
        {
            _db = topicsContext;
            _uow = new UnitOfWork(_db);
        }

        public void Add<T>(T obj)
        {
            _db.Posts.Add(Mapper.Map<Post>(obj));
            _uow.SaveChanges();
        }

        public void Change<T>(int id, T obj)
        {
            _db.Entry(Mapper.Map<Post>(obj)).State = EntityState.Modified;
            _uow.SaveChanges();
        }

        public ICollection<T> GetAll<T>()
        {
            return _db.Posts.ProjectTo<T>().ToList();
        }

        public T GetOne<T>(int id)
        {
            return _db.Posts.Where(c => c.PostID == id).ProjectTo<T>().FirstOrDefault();
        }
    }
}
