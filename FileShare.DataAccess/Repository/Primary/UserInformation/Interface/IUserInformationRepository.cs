using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.UserInformation.UserInformation;

namespace FileShare.DataAccess.Repository.Primary.User.Interface
{
    public interface IUserInformationRepository : IRepositoryBase<Model> { }
}