using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.RefreshToken.Interface;
using Microsoft.EntityFrameworkCore;
using Model = ImageApi.DataAccess.Models.Primary.RefreshToken.RefreshToken;

namespace ImageApi.DataAccess.Repository.Primary.RefreshToken
{
    public class RefreshTokenRepository : RepositoryBase<Model, PrimaryContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(PrimaryContext context) : base(context) { }


        public async Task<Model> GetFromTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await context.Set<Model>()
                .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
        }
    }
}