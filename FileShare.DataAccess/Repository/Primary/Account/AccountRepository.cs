using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.Account.Interface;
using Microsoft.EntityFrameworkCore;
using Model = FileShare.DataAccess.Models.Primary.Account.Account;

namespace FileShare.DataAccess.Repository.Primary.Account
{
    public class AccountRepository : RepositoryBase<Model, PrimaryContext>, IAccountRepository
    {
        public AccountRepository(PrimaryContext context) : base(context) { }


        public async Task<bool> IsEnabledByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.Id == id).Select(x => x.Verified).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsVerifiedByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.Id == id).Select(x => x.Verified).FirstOrDefaultAsync(cancellationToken);
        }
    }
}