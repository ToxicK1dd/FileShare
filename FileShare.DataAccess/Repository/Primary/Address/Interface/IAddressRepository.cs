using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.Address.Address;

namespace FileShare.DataAccess.Repository.Primary.Address.Interface
{
    public interface IAddressRepository : IRepositoryBase<Model> { }
}