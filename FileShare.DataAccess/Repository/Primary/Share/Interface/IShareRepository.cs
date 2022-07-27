using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.Share.Share;

namespace FileShare.DataAccess.Repository.Primary.Share.Interface
{
    public interface IShareRepository : IRepositoryBase<Model> { }
}