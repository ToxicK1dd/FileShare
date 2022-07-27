using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.Address.Interface;
using Model = FileShare.DataAccess.Models.Primary.Address.Address;

namespace FileShare.DataAccess.Repository.Primary.Address
{
    public class AddressRepository : RepositoryBase<Model, PrimaryContext>, IAddressRepository
    {
        public AddressRepository(PrimaryContext context) : base(context) { }
    }
}