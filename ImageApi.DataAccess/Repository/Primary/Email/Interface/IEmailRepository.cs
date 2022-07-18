using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Email.Email;

namespace ImageApi.DataAccess.Repository.Primary.Email.Interface
{
    public interface IEmailRepository : IRepositoryBase<Model>
    {
        Task<bool> ExistsFromAddress(string address, CancellationToken cancellationToken = default);
    }
}