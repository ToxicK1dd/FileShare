using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.Account.Account;

namespace FileShare.DataAccess.Repository.Primary.Account.Interface
{
    public interface IAccountRepository : IRepositoryBase<Model>
    {
        Task<bool> IsEnabledByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsVerifiedByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}