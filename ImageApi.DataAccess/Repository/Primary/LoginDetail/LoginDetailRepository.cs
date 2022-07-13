using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.LoginDetail.Interface;
using Model = ImageApi.DataAccess.Models.Primary.LoginDetail.LoginDetail;

namespace ImageApi.DataAccess.Repository.Primary.LoginDetail
{
    public class LoginDetailRepository : RepositoryBase<Model, PrimaryContext>, ILoginDetailRepository
    {
        public LoginDetailRepository(PrimaryContext context) : base(context) { }
    }
}