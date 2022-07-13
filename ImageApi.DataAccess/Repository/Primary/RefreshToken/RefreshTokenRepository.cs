using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.RefreshToken.Interface;
using Model = ImageApi.DataAccess.Models.Primary.RefreshToken.RefreshToken;

namespace ImageApi.DataAccess.Repository.Primary.RefreshToken
{
    public class RefreshTokenRepository : RepositoryBase<Model, PrimaryContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(PrimaryContext context) : base(context) { }
    }
}