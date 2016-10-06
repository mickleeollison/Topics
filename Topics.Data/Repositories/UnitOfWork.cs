using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Data.DAL.Interfaces;
using Topics.Data.Repositories.Interfaces;

namespace Topics.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITopicsContext _db;
        private bool _disposed = false;

        public UnitOfWork(ITopicsContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
