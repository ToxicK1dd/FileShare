using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.Share.Interface;
using Model = FileShare.DataAccess.Models.Primary.Share.Share;

namespace FileShare.DataAccess.Repository.Primary.Share
{
    public class ShareRepository : RepositoryBase<Model, PrimaryContext>, IShareRepository
    {
        public ShareRepository(PrimaryContext context) : base(context) { }
    }
}