using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.SocialSecurityNumber.Interface;
using Model = FileShare.DataAccess.Models.Primary.SocialSecurityNumber.SocialSecurityNumber;

namespace FileShare.DataAccess.Repository.Primary.SocialSecurityNumber
{
    public class SocialSecurityNumberRepository : RepositoryBase<Model, PrimaryContext>, ISocialSecurityNumberRepository
    {
        public SocialSecurityNumberRepository(PrimaryContext context) : base(context) { }
    }
}