using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.ContactInfo.ContactInfo;

namespace ImageApi.DataAccess.Repository.Primary.ContactInfo.Interface
{
    public interface IContactInfoRepository : IRepositoryBase<Model> { }
}