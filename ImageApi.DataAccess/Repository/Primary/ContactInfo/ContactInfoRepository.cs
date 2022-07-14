using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.ContactInfo.Interface;
using Microsoft.EntityFrameworkCore;
using Model = ImageApi.DataAccess.Models.Primary.ContactInfo.ContactInfo;

namespace ImageApi.DataAccess.Repository.Primary.ContactInfo
{
    public class ContactInfoRepository : RepositoryBase<Model, PrimaryContext>, IContactInfoRepository
    {
        public ContactInfoRepository(PrimaryContext context) : base(context) { }


        public Task<bool> ExistsFromEmail(string email, CancellationToken cancellationToken = default)
        {
            return context.Set<Model>().Where(x => x.Email == email).Select(x => x.Id).AnyAsync(cancellationToken);
        }

        public Task<bool> ExistsFromPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default)
        {
            return context.Set<Model>().Where(x => x.PhoneNumber == phoneNumber).Select(x => x.Id).AnyAsync(cancellationToken);
        }
    }
}