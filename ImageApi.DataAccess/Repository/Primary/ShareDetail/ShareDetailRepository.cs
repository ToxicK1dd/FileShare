using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.ShareDetail.Interface;
using Model = ImageApi.DataAccess.Models.Primary.ShareDetail.ShareDetail;

namespace ImageApi.DataAccess.Repository.Primary.ShareDetail
{
    public class ShareDetailRepository : RepositoryBase<Model, PrimaryContext>, IShareDetailRepository
    {
        public ShareDetailRepository(PrimaryContext context) : base(context) { }
    }
}