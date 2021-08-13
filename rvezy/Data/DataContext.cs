using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rvezy.Models;

namespace rvezy.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<Reviews> Reviewss { get; set; }
    }
}
