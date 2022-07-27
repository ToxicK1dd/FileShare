using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Account.Interface;
using Microsoft.EntityFrameworkCore;
using Model = ImageApi.DataAccess.Models.Primary.Account.Account;

namespace ImageApi.DataAccess.Repository.Primary.Account
{
    public class AccountRepository : RepositoryBase<Model, PrimaryContext>, IAccountRepository
    {
        public AccountRepository(PrimaryContext context) : base(context) { }


        public async Task<bool> IsEnabledByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Set<Model>().Where(x => x.Id == id).Select(x => x.Verified).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsVerifiedByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Set<Model>().Where(x => x.Id == id).Select(x => x.Verified).FirstOrDefaultAsync(cancellationToken);
        }
    }
}