using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.User.User;

namespace FileShare.DataAccess.Repository.Primary.User.Interface
{
    public interface IUserRepository : IRepositoryBase<Model>
    {
        Task<bool> IsEnabledByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<bool> IsVerifiedByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<Model> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<Guid> GetIdByUsernameAsync(string username, CancellationToken cancellationToken = default);
    }
}