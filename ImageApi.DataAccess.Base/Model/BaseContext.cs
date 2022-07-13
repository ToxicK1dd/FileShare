using Microsoft.EntityFrameworkCore;

namespace ImageApi.DataAccess.Base.Model
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
    }
}