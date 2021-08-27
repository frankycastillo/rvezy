using System;
using System.Linq;
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


        public override int SaveChanges()
        {
            var currentDate = DateTime.UtcNow;

            // We should never detect changes because it adds into db context all models loaded from the cache.
            // Instead we manually set ItemState.Modified when updating a modal in the repository.
            // ChangeTracker.DetectChanges();

            var changes = ChangeTracker.Entries<IBaseModel>();
            var changeList = changes.ToList();

            foreach (var entry in changeList)
            {
                var entity = entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = currentDate;
                        entry.Entity.ModifiedOn = currentDate;
                        entry.Entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = currentDate;
                        // Make sure the CreatedOn is never modified
                        entry.Entity.CreatedOn = entry.OriginalValues.GetValue<DateTime?>("CreatedOn");
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return base.SaveChanges();
        }

        #region Models

        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        #endregion
    }
}
