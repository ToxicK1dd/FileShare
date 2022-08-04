using FileShare.DataAccess.Base.UnitOfWork;
using FileShare.DataAccess.Models.Primary;
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
using FileShare.DataAccess.Repository.Primary.Account;
using FileShare.DataAccess.Repository.Primary.Account.Interface;

namespace FileShare.DataAccess.UnitOfWork.Primary
{
    public class PrimaryUnitOfWork : UnitOfWorkBase<PrimaryContext>, IPrimaryUnitOfWork
    {
        private readonly AddressRepository addressRepository;
        private readonly DeviceTokenRepository deviceTokenRepository;
        private readonly DocumentRepository documentRepository;
        private readonly DocumentDetailRepository documentDetailRepository;
        private readonly DocumentSignatureRepository documentSignatureRepository;
        private readonly RefreshTokenRepository refreshTokenRepository;
        private readonly ShareRepository shareRepository;
        private readonly ShareDetailRepository shareDetailRepository;
        private readonly SocialSecurityNumberRepository socialSecurityNumberRepository;
        private readonly UserInformation userInformationRepository;
        private readonly UserRepository userRepository;

        public PrimaryUnitOfWork(PrimaryContext context) : base(context)
        {
            addressRepository = new(context);
            deviceTokenRepository = new(context);
            documentRepository = new(context);
            documentDetailRepository = new(context);
            documentSignatureRepository = new(context);
            refreshTokenRepository = new(context);
            shareRepository = new(context);
            shareDetailRepository = new(context);
            socialSecurityNumberRepository = new(context);
            userInformationRepository = new(context);
            userRepository = new(context);
        }


        public IAddressRepository AddressRepository { get => addressRepository; }

        public IDeviceTokenRepository DeviceTokenRepository { get => deviceTokenRepository; }

        public IDocumentRepository DocumentRepository { get => documentRepository; }

        public IDocumentDetailRepository DocumentDetailRepository { get => documentDetailRepository; }

        public IDocumentSignatureRepository DocumentSignatureRepository { get => documentSignatureRepository; }

        public IRefreshTokenRepository RefreshTokenRepository { get => refreshTokenRepository; }

        public IShareRepository ShareRepository { get => shareRepository; }

        public IShareDetailRepository ShareDetailRepository { get => shareDetailRepository; }

        public ISocialSecurityNumberRepository SocialSecurityNumberRepository { get => socialSecurityNumberRepository; }

        public IUserInformationRepository UserInformationRepository { get => userInformationRepository; }

        public IUserRepository UserRepository { get => userRepository; }
    }
}