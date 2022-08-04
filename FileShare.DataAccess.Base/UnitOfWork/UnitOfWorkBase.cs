using FileShare.DataAccess.Base.Model;
using FileShare.DataAccess.Base.UnitOfWork.Interface;

namespace FileShare.DataAccess.Base.UnitOfWork
{
    /// <summary>
    /// Abstract base class for easy, and fast creation of a Unit of Work.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class UnitOfWorkBase<TContext> : IUnitOfWorkBase
        where TContext : BaseContext<TContext>
    {
        protected readonly TContext context;

        public UnitOfWorkBase(TContext context)
        {
            this.context = context;
        }


        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }


        #region Dispose
        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    await context.DisposeAsync();
                }
            }
            disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}