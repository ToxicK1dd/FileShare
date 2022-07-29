using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.Email.Interface;
using Microsoft.EntityFrameworkCore;
using Model = FileShare.DataAccess.Models.Primary.Email.Email;

namespace FileShare.DataAccess.Repository.Primary.Email
{
    public class EmailRepository : RepositoryBase<Model, PrimaryContext>, IEmailRepository
    {
        public EmailRepository(PrimaryContext context) : base(context) { }


        public Task<bool> ExistsFromAddressAsync(string address, CancellationToken cancellationToken = default)
        {
            return context.Set<Model>().Where(x => x.Address == address).Select(x => x.Id).AnyAsync(cancellationToken);
        }
    }
}