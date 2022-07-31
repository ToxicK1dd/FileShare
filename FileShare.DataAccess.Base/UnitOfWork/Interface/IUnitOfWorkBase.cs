namespace FileShare.DataAccess.Base.UnitOfWork.Interface
{
    /// <summary>
    /// Base unit of work for easy and fast development of different unit of work.
    /// </summary>
    public interface IUnitOfWorkBase : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Save all current changes made in the context to the database.
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}