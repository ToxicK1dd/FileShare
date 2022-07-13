using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.RefreshToken.RefreshToken;

namespace ImageApi.DataAccess.Repository.Primary.RefreshToken.Interface
{
    public interface IRefreshTokenRepository : IRepositoryBase<Model> { }
}