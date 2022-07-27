using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.PhoneNumber.PhoneNumber;

namespace FileShare.DataAccess.Repository.Primary.PhoneNumber.Interface
{
    public interface IPhoneNumberRepository : IRepositoryBase<Model>
    {
        Task<bool> ExistsFromNumber(string number, CancellationToken cancellationToken = default);
    }
}