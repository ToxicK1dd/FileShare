using FileShare.DataAccess.Base.Model.BaseEntity.Interface;
using Microsoft.EntityFrameworkCore;

namespace FileShare.DataAccess.Base.Model
{
    public abstract class BaseDbContext<TContext> : DbContext
         where TContext : BaseDbContext<TContext>
    {
        public BaseDbContext(DbContextOptions<TContext> options) : base(options) { }

        public BaseDbContext() { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Connection string should be injected
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply type configurations only from this namespace
            modelBuilder.ApplyConfigurationsFromAssembly(
                GetType().Assembly,
                t => t.Namespace.Contains(GetType().Namespace));
        }


        public override int SaveChanges()
        {
            CheckEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            CheckEntities();
            return base.SaveChangesAsync(cancellationToken);
        }


        #region Helpers
        /// <summary>
        /// Check and set created, deleted, or changed properties.
        /// </summary>
        private void CheckEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Deleted = false;
                        entry.Entity.Created = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Created = entry.OriginalValues.GetValue<DateTimeOffset>(nameof(entry.Entity.Created));
                        entry.Entity.Changed = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.Deleted = true;
                        entry.Entity.Created = entry.OriginalValues.GetValue<DateTimeOffset>(nameof(entry.Entity.Created));
                        entry.Entity.Changed = DateTimeOffset.UtcNow;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}