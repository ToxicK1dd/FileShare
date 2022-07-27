using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.PhoneNumberVerificationCode.Interface;
using Model = FileShare.DataAccess.Models.Primary.PhoneNumberVerificationCode.PhoneNumberVerificationCode;

namespace FileShare.DataAccess.Repository.Primary.PhoneNumberVerificationCode
{
    public class PhoneNumberVerificationCodeRepository : RepositoryBase<Model, PrimaryContext>, IPhoneNumberVerificationCodeRepository
    {
        public PhoneNumberVerificationCodeRepository(PrimaryContext context) : base(context) { }
    }
}