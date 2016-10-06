using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Data.DAL.Interfaces;
using Topics.Data.Repositories.Interfaces;

namespace Topics.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private ITopicsContext _db;
        private IUnitOfWork _uow;

        public RoleRepository(ITopicsContext RoleContext)
        {
            _db = RoleContext;
            _uow = new UnitOfWork(_db);
        }

        public ICollection<T> GetAll<T>()
        {
            return _db.Roles.ProjectTo<T>().ToList();
        }

        public T GetOne<T>(int id)
        {
            return _db.Roles.Where(c => c.RoleID == id).ProjectTo<T>().FirstOrDefault();
        }
    }
}
