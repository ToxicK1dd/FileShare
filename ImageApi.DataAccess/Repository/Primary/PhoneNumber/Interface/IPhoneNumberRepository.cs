using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.PhoneNumber.PhoneNumber;

namespace ImageApi.DataAccess.Repository.Primary.PhoneNumber.Interface
{
    public interface IPhoneNumberRepository : IRepositoryBase<Model>
    {
        Task<bool> ExistsFromNumber(string number, CancellationToken cancellationToken = default);
    }
}