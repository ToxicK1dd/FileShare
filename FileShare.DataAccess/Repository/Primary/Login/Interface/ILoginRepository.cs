using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.Login.Login;

namespace FileShare.DataAccess.Repository.Primary.Login.Interface
{
    public interface ILoginRepository : IRepositoryBase<Model>
    {
        Task<Guid> GetIdFromUsername(string username, CancellationToken cancellationToken = default);
        Task<Guid> GetAccountIdByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<Model> GetFromUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<bool> ExistsFromUsernameAsync(string username, CancellationToken cancellationToken = default);
    }
}