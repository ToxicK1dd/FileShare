using Microsoft.EntityFrameworkCore;

namespace FileShare.DataAccess.Base.Model
{
    /// <summary>
    /// Abstract base class for easy, and fast creation of databases.
    /// </summary>
    /// <typeparam name="TContext">The database context of which the data is stored.</typeparam>
    public abstract class BaseContext<TContext> : DbContext
            where TContext : BaseContext<TContext>
    {
        public BaseContext() { }

        public BaseContext(DbContextOptions<TContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Connection string should be injected
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            foreach (var entry in ChangeTracker.Entries<BaseEntity.BaseEntity>())
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