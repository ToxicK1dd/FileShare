using FileShare.DataAccess.Base.UnitOfWork.Interface;
using FileShare.DataAccess.Repository.Primary.Account.Interface;
using FileShare.DataAccess.Repository.Primary.Account;
using FileShare.DataAccess.Repository.Primary.Address.Interface;
using FileShare.DataAccess.Repository.Primary.DeviceToken.Interface;
using FileShare.DataAccess.Repository.Primary.Document.Interface;
using FileShare.DataAccess.Repository.Primary.DocumentDetail.Interface;
using FileShare.DataAccess.Repository.Primary.DocumentSignature.Interface;
using FileShare.DataAccess.Repository.Primary.RefreshToken.Interface;
using FileShare.DataAccess.Repository.Primary.Share.Interface;
using FileShare.DataAccess.Repository.Primary.ShareDetail.Interface;
using FileShare.DataAccess.Repository.Primary.SocialSecurityNumber.Interface;
using FileShare.DataAccess.Repository.Primary.User.Interface;

namespace FileShare.DataAccess.UnitOfWork.Primary.Interface
{
    public interface IPrimaryUnitOfWork : IUnitOfWorkBase
    {
        IAddressRepository AddressRepository { get; }

        IDeviceTokenRepository DeviceTokenRepository { get; }

        IDocumentRepository DocumentRepository { get; }

        IDocumentDetailRepository DocumentDetailRepository { get; }

        IDocumentSignatureRepository DocumentSignatureRepository { get; }

        IRefreshTokenRepository RefreshTokenRepository { get; }

        IShareRepository ShareRepository { get; }

        IShareDetailRepository ShareDetailRepository { get; }

        ISocialSecurityNumberRepository SocialSecurityNumberRepository { get; }

        IUserInformationRepository UserInformationRepository { get; }

        IUserRepository UserRepository { get; }
    }
}