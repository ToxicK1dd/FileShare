using FileShare.DataAccess.Base.Model.Entity.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileShare.DataAccess.Base.Model
{
    /// <summary>
    /// Abstract base class for easy, and fast creation of databases.
    /// </summary>
    /// <typeparam name="TContext">The database context of which the data is stored.</typeparam>
    public abstract class BaseIdentityDbContext<TContext, TUser> : IdentityDbContext<TUser, IdentityRole<Guid>, Guid>
        where TContext : BaseIdentityDbContext<TContext, TUser>
        where TUser : IdentityUser.BaseIdentityUser
    {
        public BaseIdentityDbContext(DbContextOptions<TContext> options) : base(options) { }

        public BaseIdentityDbContext() { }


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

            // Change schema name
            modelBuilder.HasDefaultSchema("Identity");

            // Change Identity table names
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable(name: "Role");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable(name: "UserRole");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable(name: "Claims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable(name: "Logins");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable(name: "RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable(name: "Tokens");
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