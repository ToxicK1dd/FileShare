using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.PhoneNumber.Interface;
using Microsoft.EntityFrameworkCore;
using Model = ImageApi.DataAccess.Models.Primary.PhoneNumber.PhoneNumber;

namespace ImageApi.DataAccess.Repository.Primary.PhoneNumber
{
    public class PhoneNumberRepository : RepositoryBase<Model, PrimaryContext>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(PrimaryContext context) : base(context) { }


        public Task<bool> ExistsFromNumber(string number, CancellationToken cancellationToken = default)
        {
            return context.Set<Model>().Where(x => x.Number == number).Select(x => x.Id).AnyAsync(cancellationToken);
        }
    }
}