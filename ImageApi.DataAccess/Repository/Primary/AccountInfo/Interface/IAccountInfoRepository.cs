using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.AccountInfo.AccountInfo;

namespace ImageApi.DataAccess.Repository.Primary.AccountInfo.Interface
{
    public interface IAccountInfoRepository : IRepositoryBase<Model> { }
}