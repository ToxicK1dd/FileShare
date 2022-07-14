using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.ContactInfo.ContactInfo;

namespace ImageApi.DataAccess.Repository.Primary.ContactInfo.Interface
{
    public interface IContactInfoRepository : IRepositoryBase<Model>
    {
        Task<bool> ExistsFromEmail(string email, CancellationToken cancellationToken = default);
        Task<bool> ExistsFromPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default);
    }
}