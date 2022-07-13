using ImageApi.DataAccess.Base.Model.BaseEntity;
using ImageApi.DataAccess.Base.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ImageApi.DataAccess.Base.Repository
{
    public abstract class RepositoryBase<TModel, TContext> : IRepositoryBase<TModel>
        where TModel : BaseEntity
        where TContext : DbContext, new()
    {
        protected TContext context;

        public RepositoryBase(TContext context)
        {
            this.context = context;
        }


        // Create
        public virtual async Task AddAsync(TModel model, CancellationToken cancellationToken = default)
        {
            await context.AddAsync(model, cancellationToken);
        }

        // Read
        public virtual async Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Set<TModel>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        // Update
        public virtual void Update(TModel model)
        {
            context.Update(model);
        }

        // Delete
        public virtual void Remove(TModel model)
        {
            context.Remove(model);
        }

        public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Set<TModel>().Where(x => x.Id == id).AsNoTracking().Select(x => x.Id).AnyAsync(cancellationToken);
        }
    }
}