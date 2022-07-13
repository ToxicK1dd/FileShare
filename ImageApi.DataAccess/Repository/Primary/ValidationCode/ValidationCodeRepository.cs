using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.ValidationCode.Interface;
using Model = ImageApi.DataAccess.Models.Primary.ValidationCode.ValidationCode;

namespace ImageApi.DataAccess.Repository.Primary.ValidationCode
{
    public class ValidationCodeRepository : RepositoryBase<Model, PrimaryContext>, IValidationCodeRepository
    {
        public ValidationCodeRepository(PrimaryContext context) : base(context) { }
    }
}