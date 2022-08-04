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
            //modelBuilder.HasDefaultSchema("Identity");

            // Change Identity table names
            modelBuilder.Entity<IdentityUser<Guid>>().ToTable(name: "Identity.User");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable(name: "Identity.Role");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable(name: "Identity.UserRole");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable(name: "Identity.Claims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable(name: "Identity.Logins");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable(name: "Identity.RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable(name: "Identity.Tokens");
        }
    }
}