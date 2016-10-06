using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Data.Entities;

namespace Topics.Data.DAL.Interfaces
{
    public interface ITopicsContext : IDisposable
    {
        DbSet<Post> Posts { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<UserCredentials> UserCredentials { get; set; }
        DbSet<User> Users { get; set; }

        DbEntityEntry Entry(object entity);

        int SaveChanges();
    }
}
