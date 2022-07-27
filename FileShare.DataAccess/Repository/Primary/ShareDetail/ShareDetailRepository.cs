using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.ShareDetail.Interface;
using Model = FileShare.DataAccess.Models.Primary.ShareDetail.ShareDetail;

namespace FileShare.DataAccess.Repository.Primary.ShareDetail
{
    public class ShareDetailRepository : RepositoryBase<Model, PrimaryContext>, IShareDetailRepository
    {
        public ShareDetailRepository(PrimaryContext context) : base(context) { }
    }
}