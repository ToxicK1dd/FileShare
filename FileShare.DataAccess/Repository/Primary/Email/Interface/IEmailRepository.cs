using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.Email.Email;

namespace FileShare.DataAccess.Repository.Primary.Email.Interface
{
    public interface IEmailRepository : IRepositoryBase<Model>
    {
        Task<bool> ExistsFromAddressAsync(string address, CancellationToken cancellationToken = default);
    }
}