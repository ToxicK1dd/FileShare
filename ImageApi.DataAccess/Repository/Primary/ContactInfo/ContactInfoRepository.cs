using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.ContactInfo.Interface;
using Model = ImageApi.DataAccess.Models.Primary.ContactInfo.ContactInfo;

namespace ImageApi.DataAccess.Repository.Primary.ContactInfo
{
    public class ContactInfoRepository : RepositoryBase<Model, PrimaryContext>, IContactInfoRepository
    {
        public ContactInfoRepository(PrimaryContext context) : base(context) { }
    }
}