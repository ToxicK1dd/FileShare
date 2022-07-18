using ImageApi.DataAccess.Base.UnitOfWork.Interface;
using ImageApi.DataAccess.Repository.Primary.Account.Interface;
using ImageApi.DataAccess.Repository.Primary.Address.Interface;
using ImageApi.DataAccess.Repository.Primary.DeviceToken.Interface;
using ImageApi.DataAccess.Repository.Primary.Document.Interface;
using ImageApi.DataAccess.Repository.Primary.DocumentDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.DocumentSignature.Interface;
using ImageApi.DataAccess.Repository.Primary.Email.Interface;
using ImageApi.DataAccess.Repository.Primary.EmailVerificationCode.Interface;
using ImageApi.DataAccess.Repository.Primary.Login.Interface;
using ImageApi.DataAccess.Repository.Primary.LoginDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.PhoneNumber.Interface;
using ImageApi.DataAccess.Repository.Primary.PhoneNumberVerificationCode.Interface;
using ImageApi.DataAccess.Repository.Primary.RefreshToken.Interface;
using ImageApi.DataAccess.Repository.Primary.Share.Interface;
using ImageApi.DataAccess.Repository.Primary.ShareDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.SocialSecurityNumber.Interface;
using ImageApi.DataAccess.Repository.Primary.User.Interface;
using ImageApi.DataAccess.Repository.Primary.ValidationCode.Interface;

namespace ImageApi.DataAccess.UnitOfWork.Primary.Interface
{
    public interface IPrimaryUnitOfWork : IUnitOfWorkBase
    {
        IAccountRepository AccountRepository { get; }

        IAddressRepository AddressRepository { get; }

        IDeviceTokenRepository DeviceTokenRepository { get;  }

        IDocumentRepository DocumentRepository { get; }

        IDocumentDetailRepository DocumentDetailRepository { get; }

        IDocumentSignatureRepository DocumentSignatureRepository { get; }

        IEmailRepository EmailRepository { get; }

        IEmailVerificationCodeRepository EmailVerificationCodeRepository { get; }

        ILoginRepository LoginRepository { get; }

        ILoginDetailRepository LoginDetailRepository { get; }

        IPhoneNumberRepository PhoneNumberRepository { get; }

        IPhoneNumberVerificationCodeRepository PhoneNumberVerificationCodeRepository { get; }

        IRefreshTokenRepository RefreshTokenRepository { get; }

        IShareRepository ShareRepository { get; }

        IShareDetailRepository ShareDetailRepository { get; }

        ISocialSecurityNumberRepository SocialSecurityNumberRepository { get; }

        IUserRepository UserRepository { get; }

        IVerificationCodeRepository VerificationCodeRepository { get; }
    }
}