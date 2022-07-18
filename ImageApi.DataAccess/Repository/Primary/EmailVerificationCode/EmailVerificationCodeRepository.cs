using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.EmailVerificationCode.Interface;
using Model = ImageApi.DataAccess.Models.Primary.EmailVerificationCode.EmailVerificationCode;

namespace ImageApi.DataAccess.Repository.Primary.EmailVerificationCode
{
    public class EmailVerificationCodeRepository : RepositoryBase<Model, PrimaryContext>, IEmailVerificationCodeRepository
    {
        public EmailVerificationCodeRepository(PrimaryContext context) : base(context) { }
    }
}