using ImageApi.DataAccess.Base.UnitOfWork;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Account;
using ImageApi.DataAccess.Repository.Primary.Account.Interface;
using ImageApi.DataAccess.Repository.Primary.Role;
using ImageApi.DataAccess.Repository.Primary.Role.Interface;
using ImageApi.DataAccess.Repository.Primary.Address;
using ImageApi.DataAccess.Repository.Primary.Address.Interface;
using ImageApi.DataAccess.Repository.Primary.ContactDetail;
using ImageApi.DataAccess.Repository.Primary.ContactDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.Document;
using ImageApi.DataAccess.Repository.Primary.Document.Interface;
using ImageApi.DataAccess.Repository.Primary.DocumentDetail;
using ImageApi.DataAccess.Repository.Primary.DocumentDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.DocumentSignature;
using ImageApi.DataAccess.Repository.Primary.DocumentSignature.Interface;
using ImageApi.DataAccess.Repository.Primary.Login;
using ImageApi.DataAccess.Repository.Primary.Login.Interface;
using ImageApi.DataAccess.Repository.Primary.LoginDetail;
using ImageApi.DataAccess.Repository.Primary.LoginDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.RefreshToken;
using ImageApi.DataAccess.Repository.Primary.RefreshToken.Interface;
using ImageApi.DataAccess.Repository.Primary.Share;
using ImageApi.DataAccess.Repository.Primary.Share.Interface;
using ImageApi.DataAccess.Repository.Primary.ShareDetail;
using ImageApi.DataAccess.Repository.Primary.ShareDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.SocialSecurityNumber;
using ImageApi.DataAccess.Repository.Primary.SocialSecurityNumber.Interface;
using ImageApi.DataAccess.Repository.Primary.ValidationCode;
using ImageApi.DataAccess.Repository.Primary.ValidationCode.Interface;
using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.DataAccess.Repository.Primary.PersonalDetail;
using ImageApi.DataAccess.Repository.Primary.DeviceToken;
using ImageApi.DataAccess.Repository.Primary.PersonalDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.DeviceToken.Interface;

namespace ImageApi.DataAccess.UnitOfWork.Primary
{
    public class PrimaryUnitOfWork : UnitOfWorkBase<PrimaryContext>, IPrimaryUnitOfWork
    {
        private AccountRepository accountRepository;
        private RoleRepository roleRepository;
        private AddressRepository addressRepository;
        private ContactInfoRepository contactDetailRepository;
        private DeviceTokenRepository deviceTokenRepository;
        private DocumentRepository documentRepository;
        private DocumentDetailRepository documentDetailRepository;
        private DocumentSignatureRepository documentSignatureRepository;
        private LoginRepository loginRepository;
        private LoginDetailRepository loginDetailRepository;
        private PersonalDetailRepository personalDetailRepository;
        private RefreshTokenRepository refreshTokenRepository;
        private ShareRepository shareRepository;
        private ShareDetailRepository shareDetailRepository;
        private SocialSecurityNumberRepository socialSecurityNumberRepository;
        private VerificationCodeRepository verificationCodeRepository;

        public PrimaryUnitOfWork(PrimaryContext context) : base(context) { }


        public IAccountRepository AccountRepository { get => accountRepository ??= new(context); }

        public IRoleRepository RoleRepository { get => roleRepository ??= new(context); }

        public IAddressRepository AddressRepository { get => addressRepository ??= new(context); }

        public IContactDetailRepository ContactDetailRepository { get => contactDetailRepository ??= new(context); }

        public IDeviceTokenRepository DeviceTokenRepository { get => deviceTokenRepository ??= new(context); }

        public IDocumentRepository DocumentRepository { get => documentRepository ??= new(context); }

        public IDocumentDetailRepository DocumentDetailRepository { get => documentDetailRepository ??= new(context); }

        public IDocumentSignatureRepository DocumentSignatureRepository { get => documentSignatureRepository ??= new(context); }

        public ILoginRepository LoginRepository { get => loginRepository ??= new(context); }

        public ILoginDetailRepository LoginDetailRepository { get => loginDetailRepository ??= new(context); }

        public IPersonalDetailRepository PersonalDetailRepository { get => personalDetailRepository ??= new(context); }

        public IRefreshTokenRepository RefreshTokenRepository { get => refreshTokenRepository ??= new(context); }

        public IShareRepository ShareRepository { get => shareRepository ??= new(context); }

        public IShareDetailRepository ShareDetailRepository { get => shareDetailRepository ??= new(context); }

        public ISocialSecurityNumberRepository SocialSecurityNumberRepository { get => socialSecurityNumberRepository ??= new(context); }

        public IVerificationCodeRepository VerificationCodeRepository { get => verificationCodeRepository ??= new(context); }
    }
}