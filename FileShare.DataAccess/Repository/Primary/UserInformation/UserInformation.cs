using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.User.Interface;
using Model = FileShare.DataAccess.Models.Primary.UserInformation.UserInformation;

namespace FileShare.DataAccess.Repository.Primary.User
{
    public class UserInformation : RepositoryBase<Model, PrimaryContext>, IUserInformationRepository
    {
        public UserInformation(PrimaryContext context) : base(context) { }
    }
}