using ActivitiesList.Data.Mappings;
using ActivitiesList.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivitiesList.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :
            base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityMap());
        }
    }
}
