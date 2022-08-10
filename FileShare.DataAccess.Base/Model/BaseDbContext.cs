using FileShare.DataAccess.Base.Model.Entity.Interface;
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
            foreach (var entry in ChangeTracker.Entries<ICreatable>().Where(x => x.State is EntityState.Added))
            {
                entry.Entity.Created = DateTimeOffset.UtcNow;
            }
            foreach (var entry in ChangeTracker.Entries<IChangeable>().Where(x => x.State is EntityState.Modified))
            {
                entry.Entity.Changed = DateTimeOffset.UtcNow;
            }
            foreach (var entry in ChangeTracker.Entries<ISoftDeletable>().Where(x => x.State is EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.Deleted = DateTimeOffset.UtcNow;
            }
        }

        #endregion
    }
}