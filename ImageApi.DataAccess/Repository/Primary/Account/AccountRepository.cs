using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Account.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Account.Account;

namespace ImageApi.DataAccess.Repository.Primary.Account
{
    public class AccountRepository : RepositoryBase<Model, PrimaryContext>, IAccountRepository
    {
        public AccountRepository(PrimaryContext context) : base(context) { }
    }
}