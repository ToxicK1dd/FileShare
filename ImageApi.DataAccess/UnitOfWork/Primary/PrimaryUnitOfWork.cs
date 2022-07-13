using ImageApi.DataAccess.Base.UnitOfWork;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Account;
using ImageApi.DataAccess.Repository.Primary.Account.Interface;
using ImageApi.DataAccess.Repository.Primary.AccountInfo;
using ImageApi.DataAccess.Repository.Primary.AccountInfo.Interface;
using ImageApi.DataAccess.Repository.Primary.AccountRole;
using ImageApi.DataAccess.Repository.Primary.AccountRole.Interface;
using ImageApi.DataAccess.Repository.Primary.Address;
using ImageApi.DataAccess.Repository.Primary.Address.Interface;
using ImageApi.DataAccess.Repository.Primary.ContactInfo;
using ImageApi.DataAccess.Repository.Primary.ContactInfo.Interface;
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

namespace ImageApi.DataAccess.UnitOfWork.Primary
{
    public class PrimaryUnitOfWork : UnitOfWorkBase<PrimaryContext>, IPrimaryUnitOfWork
    {
        private AccountRepository accountRepository;
        private AccountInfoRepository accountInfoRepository;
        private AccountRoleRepository accountRoleRepository;
        private AddressRepository addressRepository;
        private ContactInfoRepository contactInfoRepository;
        private DocumentRepository documentRepository;
        private DocumentDetailRepository documentDetailRepository;
        private DocumentSignatureRepository documentSignatureRepository;
        private LoginRepository loginRepository;
        private LoginDetailRepository loginDetailRepository;
        private RefreshTokenRepository refreshTokenRepository;
        private ShareRepository shareRepository;
        private ShareDetailRepository shareDetailRepository;
        private SocialSecurityNumberRepository socialSecurityNumberRepository;
        private ValidationCodeRepository validationCodeRepository;

        public PrimaryUnitOfWork(PrimaryContext context) : base(context) { }


        public IAccountRepository AccountRepository { get => accountRepository ??= new(context); }

        public IAccountInfoRepository AccountInfoRepository { get => accountInfoRepository ??= new(context); }

        public IAccountRoleRepository AccountRoleRepository { get => accountRoleRepository ??= new(context); }

        public IAddressRepository AddressRepository { get => addressRepository ??= new(context); }

        public IContactInfoRepository ContactInfoRepository { get => contactInfoRepository ??= new(context); }

        public IDocumentRepository DocumentRepository { get => documentRepository ??= new(context); }

        public IDocumentDetailRepository DocumentDetailRepository { get => documentDetailRepository ??= new(context); }

        public IDocumentSignatureRepository DocumentSignatureRepository { get => documentSignatureRepository ??= new(context); }

        public ILoginRepository LoginRepository { get => loginRepository ??= new(context); }

        public ILoginDetailRepository LoginDetailRepository { get => loginDetailRepository ??= new(context); }

        public IRefreshTokenRepository RefreshTokenRepository { get => refreshTokenRepository ??= new(context); }

        public IShareRepository ShareRepository { get => shareRepository ??= new(context); }

        public IShareDetailRepository ShareDetailRepository { get => shareDetailRepository ??= new(context); }

        public ISocialSecurityNumberRepository SocialSecurityNumberRepository { get => socialSecurityNumberRepository ??= new(context); }

        public IValidationCodeRepository ValidationCodeRepository { get => validationCodeRepository ??= new(context); }
    }
}