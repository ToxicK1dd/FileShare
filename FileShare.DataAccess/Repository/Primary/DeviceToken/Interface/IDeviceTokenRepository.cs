using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.DeviceToken.DeviceToken;

namespace FileShare.DataAccess.Repository.Primary.DeviceToken.Interface
{
    public interface IDeviceTokenRepository : IRepositoryBase<Model> { }
}