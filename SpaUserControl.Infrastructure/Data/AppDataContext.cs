using SpaUserControl.Domain.Models;
using SpaUserControl.Infrastructure.Map;
using System.Data.Entity;

namespace SpaUserControl.Infrastructure.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext() : base("AppConnectionString")
        {
            //Database.CreateIfNotExists();
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}