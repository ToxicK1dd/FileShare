using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Login.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Login.Login;

namespace ImageApi.DataAccess.Repository.Primary.Login
{
    public class LoginRepository : RepositoryBase<Model, PrimaryContext>, ILoginRepository
    {
        public LoginRepository(PrimaryContext context) : base(context) { }
    }
}