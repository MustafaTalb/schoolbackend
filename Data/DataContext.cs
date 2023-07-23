using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Area> Areas => Set<Area>();
        public DbSet<Street> Streets => Set<Street>();
        public DbSet<Address> Addresses => Set<Address>();
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

    }
}