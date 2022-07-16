using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Role.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Role.Role;

namespace ImageApi.DataAccess.Repository.Primary.Role
{
    public class RoleRepository : RepositoryBase<Model, PrimaryContext>, IRoleRepository
    {
        public RoleRepository(PrimaryContext context) : base(context) { }
    }
}