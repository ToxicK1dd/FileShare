using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.DeviceToken.Interface;
using Model = ImageApi.DataAccess.Models.Primary.DeviceToken.DeviceToken;

namespace ImageApi.DataAccess.Repository.Primary.DeviceToken
{
    public class DeviceTokenRepository : RepositoryBase<Model, PrimaryContext>, IDeviceTokenRepository
    {
        public DeviceTokenRepository(PrimaryContext context) : base(context) { }
    }
}