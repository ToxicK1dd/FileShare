using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.User.Interface;
using Model = FileShare.DataAccess.Models.Primary.User.User;

namespace FileShare.DataAccess.Repository.Primary.User
{
    public class UserRepository : RepositoryBase<Model, PrimaryContext>, IUserRepository
    {
        public UserRepository(PrimaryContext context) : base(context) { }
    }
}