using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.DeviceToken.DeviceToken;

namespace ImageApi.DataAccess.Repository.Primary.DeviceToken.Interface
{
    public interface IDeviceTokenRepository : IRepositoryBase<Model> { }
}