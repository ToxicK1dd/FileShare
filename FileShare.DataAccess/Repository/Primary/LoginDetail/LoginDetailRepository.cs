using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.LoginDetail.Interface;
using Model = FileShare.DataAccess.Models.Primary.LoginDetail.LoginDetail;

namespace FileShare.DataAccess.Repository.Primary.LoginDetail
{
    public class LoginDetailRepository : RepositoryBase<Model, PrimaryContext>, ILoginDetailRepository
    {
        public LoginDetailRepository(PrimaryContext context) : base(context) { }
    }
}