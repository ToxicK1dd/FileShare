using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.DeviceToken.Interface;
using Model = FileShare.DataAccess.Models.Primary.DeviceToken.DeviceToken;

namespace FileShare.DataAccess.Repository.Primary.DeviceToken
{
    public class DeviceTokenRepository : RepositoryBase<Model, PrimaryContext>, IDeviceTokenRepository
    {
        public DeviceTokenRepository(PrimaryContext context) : base(context) { }
    }
}