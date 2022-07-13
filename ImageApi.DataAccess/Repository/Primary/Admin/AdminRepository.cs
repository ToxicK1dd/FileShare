using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Admin.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Admin.Admin;

namespace ImageApi.DataAccess.Repository.Primary.Admin
{
    public class AdminRepository : RepositoryBase<Model, PrimaryContext>, IAdminRepository
    {
        public AdminRepository(PrimaryContext context) : base(context) { }
    }
}