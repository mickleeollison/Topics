using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Data.DAL.Interfaces;
using Topics.Data.Entities;
using Topics.Data.Repositories;
using Topics.Data.Repositories.Interfaces;

namespace Topics.Data.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private ITopicsContext _db;
        private IUnitOfWork _uow;

        public TopicRepository(ITopicsContext TopicContext)
        {
            _db = TopicContext;
            _uow = new UnitOfWork(_db);
        }

        public void Add<T>(T obj)
        {
            _db.Topics.Add(Mapper.Map<Topic>(obj));
            _uow.SaveChanges();
        }

        public void Change<T>(int id, T obj)
        {
            _db.Entry(Mapper.Map<Topic>(obj)).State = EntityState.Modified;
            _uow.SaveChanges();
        }

        public ICollection<T> GetAll<T>()
        {
            return _db.Topics.ProjectTo<T>().ToList();
        }

        public T GetOne<T>(int id)
        {
            return _db.Topics.Where(c => c.TopicID == id).ProjectTo<T>().FirstOrDefault();
        }
    }
}
