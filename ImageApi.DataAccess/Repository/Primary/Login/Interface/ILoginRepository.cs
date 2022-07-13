using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Login.Login;

namespace ImageApi.DataAccess.Repository.Primary.Login.Interface
{
    public interface ILoginRepository : IRepositoryBase<Model> { }
}