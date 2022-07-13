using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.AccountInfo.Interface;
using Model = ImageApi.DataAccess.Models.Primary.AccountInfo.AccountInfo;

namespace ImageApi.DataAccess.Repository.Primary.AccountInfo
{
    public class AccountInfoRepository : RepositoryBase<Model, PrimaryContext>, IAccountInfoRepository
    {
        public AccountInfoRepository(PrimaryContext context) : base(context) { }
    }
}