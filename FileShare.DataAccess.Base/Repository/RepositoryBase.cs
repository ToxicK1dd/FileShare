using FileShare.DataAccess.Base.Model;
using FileShare.DataAccess.Base.Model.BaseEntity;
using FileShare.DataAccess.Base.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FileShare.DataAccess.Base.Repository
{
    /// <summary>
    /// Abstract base class for easy, and fast creation of repositories.
    /// </summary>
    /// <typeparam name="TModel">The database model which the repository manages.</typeparam>
    /// <typeparam name="TContext">The database context which the repository makes the changes to.</typeparam>
    public abstract class RepositoryBase<TModel, TContext> : IRepositoryBase<TModel>
        where TModel : BaseEntity
        where TContext : BaseContext<TContext>, new()
    {
        protected TContext context;
        internal DbSet<TModel> dbSet;

        public RepositoryBase(TContext context)
        {
            this.context = context;
            dbSet = context.Set<TModel>();
        }


        // Create
        public virtual async Task AddAsync(TModel model, CancellationToken cancellationToken = default)
        {
            await dbSet.AddAsync(model, cancellationToken);
        }

        // Read
        public virtual async Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        // Update
        public virtual void Update(TModel model)
        {
            dbSet.Update(model);
        }

        // Delete
        public virtual void Remove(TModel model)
        {
            dbSet.Remove(model);
        }

        // Delete
        public virtual void RemoveById(Guid id)
        {
            var model = dbSet.Where(x => x.Id == id).FirstOrDefault();
            dbSet.Remove(model);
        }

        // Exists
        public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.Id == id).AsNoTracking().Select(x => x.Id).AnyAsync(cancellationToken);
        }
    }
}