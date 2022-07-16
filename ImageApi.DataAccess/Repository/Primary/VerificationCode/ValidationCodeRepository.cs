using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.ValidationCode.Interface;
using Model = ImageApi.DataAccess.Models.Primary.VerificationCode.VerificationCode;

namespace ImageApi.DataAccess.Repository.Primary.ValidationCode
{
    public class VerificationCodeRepository : RepositoryBase<Model, PrimaryContext>, IVerificationCodeRepository
    {
        public VerificationCodeRepository(PrimaryContext context) : base(context) { }
    }
}