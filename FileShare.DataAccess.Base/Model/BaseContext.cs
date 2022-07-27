using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace FileShare.DataAccess.Base.Model
{
    public abstract class BaseContext<TContext> : DbContext
            where TContext : DbContext
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