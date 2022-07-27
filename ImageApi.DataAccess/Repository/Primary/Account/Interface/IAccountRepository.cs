using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Account.Account;

namespace ImageApi.DataAccess.Repository.Primary.Account.Interface
{
    public interface IAccountRepository : IRepositoryBase<Model>
    {
        Task<bool> IsEnabledByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsVerifiedByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}