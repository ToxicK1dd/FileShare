using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.RefreshToken.RefreshToken;

namespace FileShare.DataAccess.Repository.Primary.RefreshToken.Interface
{
    public interface IRefreshTokenRepository : IRepositoryBase<Model>
    {
        Task<Model> GetFromTokenAsync(string token, CancellationToken cancellationToken = default);
        Task<Guid> GetUserIdFromToken(string token, CancellationToken cancellation = default);
    }
}