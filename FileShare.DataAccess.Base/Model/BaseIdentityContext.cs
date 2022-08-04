using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileShare.DataAccess.Base.Model
{
    public class BaseIdentityContext<TContext> : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
        where TContext : BaseIdentityContext<TContext>
    {
        public BaseIdentityContext(DbContextOptions<TContext> options) : base(options) { }
        
        public BaseIdentityContext() { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Connection string should be injected
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change schema name
            modelBuilder.HasDefaultSchema("Identity");

            // Change Identity table names
            modelBuilder.Entity<IdentityUser<Guid>>().ToTable(name: "User");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable(name: "Role");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable(name: "UserRole");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable(name: "Claims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable(name: "Logins");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable(name: "RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable(name: "Tokens");
        }
    }
}