using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.User.Interface;
using Microsoft.EntityFrameworkCore;
using Model = FileShare.DataAccess.Models.Primary.User.User;

namespace FileShare.DataAccess.Repository.Primary.User
{
    public class UserRepository : RepositoryBase<Model, PrimaryContext>, IUserRepository
    {
        public UserRepository(PrimaryContext context) : base(context) { }


        public async Task<bool> IsEnabledByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.UserName == username).Select(x => x.IsEnabled).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsVerifiedByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.UserName == username).Select(x => x.IsVerified).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Model> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.UserName == username).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Guid> GetIdByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}