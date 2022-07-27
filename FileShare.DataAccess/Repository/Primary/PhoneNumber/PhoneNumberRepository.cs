using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.PhoneNumber.Interface;
using Microsoft.EntityFrameworkCore;
using Model = FileShare.DataAccess.Models.Primary.PhoneNumber.PhoneNumber;

namespace FileShare.DataAccess.Repository.Primary.PhoneNumber
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