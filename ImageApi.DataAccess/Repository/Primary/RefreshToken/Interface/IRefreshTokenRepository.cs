using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.RefreshToken.RefreshToken;

namespace ImageApi.DataAccess.Repository.Primary.RefreshToken.Interface
{
    public interface IRefreshTokenRepository : IRepositoryBase<Model> 
    {
        Task<Model> GetFromTokenAsync(string token, CancellationToken cancellationToken = default);
        Task<Guid> GetAccountIdFromToken(string token, CancellationToken cancellation = default);
    }
}