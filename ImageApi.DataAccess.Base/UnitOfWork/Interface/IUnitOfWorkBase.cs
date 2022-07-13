namespace ImageApi.DataAccess.Base.UnitOfWork.Interface
{
    /// <summary>
    /// Base unit of work for easy and fast development of different unit of work.
    /// </summary>
    public interface IUnitOfWorkBase : IDisposable
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}