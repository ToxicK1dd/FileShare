using FileShare.DataAccess.Base.UnitOfWork;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.Account.Interface;
using FileShare.DataAccess.Repository.Primary.Account;
using FileShare.DataAccess.Repository.Primary.Address.Interface;
using FileShare.DataAccess.Repository.Primary.Address;
using FileShare.DataAccess.Repository.Primary.DeviceToken.Interface;
using FileShare.DataAccess.Repository.Primary.DeviceToken;
using FileShare.DataAccess.Repository.Primary.Document.Interface;
using FileShare.DataAccess.Repository.Primary.Document;
using FileShare.DataAccess.Repository.Primary.DocumentDetail.Interface;
using FileShare.DataAccess.Repository.Primary.DocumentDetail;
using FileShare.DataAccess.Repository.Primary.DocumentSignature.Interface;
using FileShare.DataAccess.Repository.Primary.DocumentSignature;
using FileShare.DataAccess.Repository.Primary.Email;
using FileShare.DataAccess.Repository.Primary.EmailVerificationCode;
using FileShare.DataAccess.Repository.Primary.Login.Interface;
using FileShare.DataAccess.Repository.Primary.Login;
using FileShare.DataAccess.Repository.Primary.LoginDetail.Interface;
using FileShare.DataAccess.Repository.Primary.LoginDetail;
using FileShare.DataAccess.Repository.Primary.RefreshToken.Interface;
using FileShare.DataAccess.Repository.Primary.RefreshToken;
using FileShare.DataAccess.Repository.Primary.Share.Interface;
using FileShare.DataAccess.Repository.Primary.Share;
using FileShare.DataAccess.Repository.Primary.ShareDetail.Interface;
using FileShare.DataAccess.Repository.Primary.ShareDetail;
using FileShare.DataAccess.Repository.Primary.SocialSecurityNumber.Interface;
using FileShare.DataAccess.Repository.Primary.SocialSecurityNumber;
using FileShare.DataAccess.Repository.Primary.User.Interface;
using FileShare.DataAccess.Repository.Primary.User;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.DataAccess.Repository.Primary.Email.Interface;
using FileShare.DataAccess.Repository.Primary.EmailVerificationCode.Interface;
using FileShare.DataAccess.Repository.Primary.PhoneNumberVerificationCode.Interface;
using FileShare.DataAccess.Repository.Primary.PhoneNumber.Interface;
using FileShare.DataAccess.Repository.Primary.PhoneNumberVerificationCode;
using FileShare.DataAccess.Repository.Primary.PhoneNumber;

namespace FileShare.DataAccess.UnitOfWork.Primary
{
    public class PrimaryUnitOfWork : UnitOfWorkBase<PrimaryContext>, IPrimaryUnitOfWork
    {
        private AccountRepository accountRepository;
        private AddressRepository addressRepository;
        private DeviceTokenRepository deviceTokenRepository;
        private DocumentRepository documentRepository;
        private DocumentDetailRepository documentDetailRepository;
        private DocumentSignatureRepository documentSignatureRepository;
        private EmailRepository emailRepository;
        private EmailVerificationCodeRepository emailVerificationCodeRepository;
        private LoginRepository loginRepository;
        private LoginDetailRepository loginDetailRepository;
        private PhoneNumberRepository phoneNumberRepository;
        private PhoneNumberVerificationCodeRepository phoneNumberVerificationCodeRepository;
        private RefreshTokenRepository refreshTokenRepository;
        private ShareRepository shareRepository;
        private ShareDetailRepository shareDetailRepository;
        private SocialSecurityNumberRepository socialSecurityNumberRepository;
        private UserRepository userRepository;

        public PrimaryUnitOfWork(PrimaryContext context) : base(context) { }


        public IAccountRepository AccountRepository { get => accountRepository ??= new(context); }

        public IAddressRepository AddressRepository { get => addressRepository ??= new(context); }

        public IDeviceTokenRepository DeviceTokenRepository { get => deviceTokenRepository ??= new(context); }

        public IDocumentRepository DocumentRepository { get => documentRepository ??= new(context); }

        public IDocumentDetailRepository DocumentDetailRepository { get => documentDetailRepository ??= new(context); }

        public IDocumentSignatureRepository DocumentSignatureRepository { get => documentSignatureRepository ??= new(context); }

        public IEmailRepository EmailRepository { get => emailRepository ??= new(context); }

        public IEmailVerificationCodeRepository EmailVerificationCodeRepository { get => emailVerificationCodeRepository ??= new(context); }

        public ILoginRepository LoginRepository { get => loginRepository ??= new(context); }

        public ILoginDetailRepository LoginDetailRepository { get => loginDetailRepository ??= new(context); }

        public IPhoneNumberRepository PhoneNumberRepository { get => phoneNumberRepository ??= new(context); }

        public IPhoneNumberVerificationCodeRepository PhoneNumberVerificationCodeRepository { get => phoneNumberVerificationCodeRepository ??= new(context); }

        public IRefreshTokenRepository RefreshTokenRepository { get => refreshTokenRepository ??= new(context); }

        public IShareRepository ShareRepository { get => shareRepository ??= new(context); }

        public IShareDetailRepository ShareDetailRepository { get => shareDetailRepository ??= new(context); }

        public ISocialSecurityNumberRepository SocialSecurityNumberRepository { get => socialSecurityNumberRepository ??= new(context); }

        public IUserRepository UserRepository { get => userRepository ??= new(context); }
    }
}