using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.User.User;

namespace ImageApi.DataAccess.Repository.Primary.User.Interface
{
    public interface IUserRepository : IRepositoryBase<Model> { }
}