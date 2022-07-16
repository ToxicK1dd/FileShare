using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.ContactDetail.ContactDetail;

namespace ImageApi.DataAccess.Repository.Primary.ContactDetail.Interface
{
    public interface IContactDetailRepository : IRepositoryBase<Model>
    {
        Task<bool> ExistsFromEmail(string email, CancellationToken cancellationToken = default);
        Task<bool> ExistsFromPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default);
    }
}