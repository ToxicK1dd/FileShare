using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.EmailVerificationCode.Interface;
using Model = FileShare.DataAccess.Models.Primary.EmailVerificationCode.EmailVerificationCode;

namespace FileShare.DataAccess.Repository.Primary.EmailVerificationCode
{
    public class EmailVerificationCodeRepository : RepositoryBase<Model, PrimaryContext>, IEmailVerificationCodeRepository
    {
        public EmailVerificationCodeRepository(PrimaryContext context) : base(context) { }
    }
}