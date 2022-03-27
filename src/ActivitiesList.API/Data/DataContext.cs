using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesList.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivitiesList.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Activity> Activities { get; set; }
    }
}