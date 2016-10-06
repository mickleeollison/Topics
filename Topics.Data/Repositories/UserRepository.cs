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
    public class UserRepository : IUserRepository
    {
        private ITopicsContext _db;
        private IUnitOfWork _uow;

        public UserRepository(ITopicsContext UserContext)
        {
            _db = UserContext;
            _uow = new UnitOfWork(_db);
        }

        public void Add<T>(T obj)
        {
            _db.Users.Add(Mapper.Map<User>(obj));
            _uow.SaveChanges();
        }

        public void AddUserCredentials<T>(T userCredentials)
        {
            _db.UserCredentials.Add(Mapper.Map<UserCredentials>(userCredentials));
            _uow.SaveChanges();
        }

        public void Change<T>(int id, T obj)
        {
            _db.Entry(Mapper.Map<User>(obj)).State = EntityState.Modified;
            _uow.SaveChanges();
        }

        public ICollection<T> GetAll<T>()
        {
            return _db.Users.ProjectTo<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            return _db.Users.Where(c => c.UserName == name).ProjectTo<T>().FirstOrDefault();
        }

        public T GetOne<T>(int id)
        {
            return _db.Users.Where(c => c.UserID == id).ProjectTo<T>().FirstOrDefault();
        }

        public T GetUserCredentialsByUserID<T>(int id)
        {
            return _db.UserCredentials.Where(c => c.UserID == id).ProjectTo<T>().FirstOrDefault();
        }
    }
}
