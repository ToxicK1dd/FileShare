using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Address.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Address.Address;

namespace ImageApi.DataAccess.Repository.Primary.Address
{
    public class AddressRepository : RepositoryBase<Model, PrimaryContext>, IAddressRepository
    {
        public AddressRepository(PrimaryContext context) : base(context) { }
    }
}