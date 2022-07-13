using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Account.Account;

namespace ImageApi.DataAccess.Repository.Primary.Account.Interface
{
    public interface IAccountRepository : IRepositoryBase<Model> { }
}