using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.PhoneNumberVerificationCode.Interface;
using Model = ImageApi.DataAccess.Models.Primary.PhoneNumberVerificationCode.PhoneNumberVerificationCode;

namespace ImageApi.DataAccess.Repository.Primary.PhoneNumberVerificationCode
{
    public class PhoneNumberVerificationCodeRepository : RepositoryBase<Model, PrimaryContext>, IPhoneNumberVerificationCodeRepository
    {
        public PhoneNumberVerificationCodeRepository(PrimaryContext context) : base(context) { }
    }
}