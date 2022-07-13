using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.SocialSecurityNumber.Interface;
using Model = ImageApi.DataAccess.Models.Primary.SocialSecurityNumber.SocialSecurityNumber;

namespace ImageApi.DataAccess.Repository.Primary.SocialSecurityNumber
{
    public class SocialSecurityNumberRepository : RepositoryBase<Model, PrimaryContext>, ISocialSecurityNumberRepository
    {
        public SocialSecurityNumberRepository(PrimaryContext context) : base(context) { }
    }
}