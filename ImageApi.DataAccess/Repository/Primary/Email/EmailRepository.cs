using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Email.Interface;
using Microsoft.EntityFrameworkCore;
using Model = ImageApi.DataAccess.Models.Primary.Email.Email;

namespace ImageApi.DataAccess.Repository.Primary.Email
{
    public class EmailRepository : RepositoryBase<Model, PrimaryContext>, IEmailRepository
    {
        public EmailRepository(PrimaryContext context) : base(context) { }


        public Task<bool> ExistsFromAddress(string address, CancellationToken cancellationToken = default)
        {
            return context.Set<Model>().Where(x => x.Address == address).Select(x => x.Id).AnyAsync(cancellationToken);
        }
    }
}