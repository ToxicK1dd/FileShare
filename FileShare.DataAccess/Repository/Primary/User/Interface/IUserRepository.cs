using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.User.User;

namespace FileShare.DataAccess.Repository.Primary.User.Interface
{
    public interface IUserRepository : IRepositoryBase<Model> { }
}