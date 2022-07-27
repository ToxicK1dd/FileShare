using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.LoginDetail.LoginDetail;

namespace FileShare.DataAccess.Repository.Primary.LoginDetail.Interface
{
    public interface ILoginDetailRepository : IRepositoryBase<Model> { }
}