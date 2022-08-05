using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.RefreshToken.Interface;
using Microsoft.EntityFrameworkCore;
using Model = FileShare.DataAccess.Models.Primary.RefreshToken.RefreshToken;

namespace FileShare.DataAccess.Repository.Primary.RefreshToken
{
    public class RefreshTokenRepository : RepositoryBase<Model, PrimaryContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(PrimaryContext context) : base(context) { }


        public async Task<Model> GetFromTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await dbSet
                .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
        }

        public async Task<Guid> GetUserIdFromToken(string token, CancellationToken cancellation = default)
        {
            return await dbSet
                .Where(x => x.Token == token)
                .Include(x => x.User)
                .Select(x => x.User.Id)
                .FirstOrDefaultAsync(cancellation);
        }
    }
}