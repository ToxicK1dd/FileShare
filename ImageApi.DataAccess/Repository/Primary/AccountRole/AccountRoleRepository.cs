using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.AccountRole.Interface;
using Model = ImageApi.DataAccess.Models.Primary.AccountRole.AccountRole;

namespace ImageApi.DataAccess.Repository.Primary.AccountRole
{
    public class AccountRoleRepository : RepositoryBase<Model, PrimaryContext>, IAccountRoleRepository
    {
        public AccountRoleRepository(PrimaryContext context) : base(context) { }
    }
}