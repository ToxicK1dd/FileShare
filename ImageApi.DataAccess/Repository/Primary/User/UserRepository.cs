using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.User.Interface;
using Model = ImageApi.DataAccess.Models.Primary.User.User;

namespace ImageApi.DataAccess.Repository.Primary.User
{
    public class UserRepository : RepositoryBase<Model, PrimaryContext>, IUserRepository
    {
        public UserRepository(PrimaryContext context) : base(context) { }
    }
}