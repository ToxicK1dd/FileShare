using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.Login.Interface;
using Microsoft.EntityFrameworkCore;
using Model = FileShare.DataAccess.Models.Primary.Login.Login;

namespace FileShare.DataAccess.Repository.Primary.Login
{
    public class LoginRepository : RepositoryBase<Model, PrimaryContext>, ILoginRepository
    {
        public LoginRepository(PrimaryContext context) : base(context) { }


        public async Task<Guid> GetIdFromUsername(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.Username == username).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Guid> GetAccountIdByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.Username == username).Select(x => x.AccountId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Model> GetFromUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet
                .Where(x => x.Username == username && x.Account.Enabled)
                .Include(x => x.Account)
                .Select(x => new Model()
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    Username = x.Username,
                    Password = x.Password,
                    Account = new()
                    {
                        Enabled = x.Account.Enabled
                    }
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> ExistsFromUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.Username == username).Select(x => x.Id).AnyAsync(cancellationToken);
        }
    }
}