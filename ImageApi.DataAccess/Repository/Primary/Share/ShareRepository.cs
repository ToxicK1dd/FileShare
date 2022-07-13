using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Share.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Share.Share;

namespace ImageApi.DataAccess.Repository.Primary.Share
{
    public class ShareRepository : RepositoryBase<Model, PrimaryContext>, IShareRepository
    {
        public ShareRepository(PrimaryContext context) : base(context) { }
    }
}