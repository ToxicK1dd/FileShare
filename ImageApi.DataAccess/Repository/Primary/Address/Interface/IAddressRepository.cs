using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Address.Address;

namespace ImageApi.DataAccess.Repository.Primary.Address.Interface
{
    public interface IAddressRepository : IRepositoryBase<Model> { }
}