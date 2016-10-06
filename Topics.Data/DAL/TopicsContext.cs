using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Topics.Data.DAL.Interfaces;
using Topics.Data.Entities;

namespace Topics.Data.DAL
{
    public class TopicsContext : DbContext, ITopicsContext
    {
        public TopicsContext() : base("TopicsContext")
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
