using ImageApi.DataAccess.Base.UnitOfWork.Interface;
using ImageApi.DataAccess.Repository.Primary.Account.Interface;
using ImageApi.DataAccess.Repository.Primary.AccountInfo.Interface;
using ImageApi.DataAccess.Repository.Primary.AccountRole.Interface;
using ImageApi.DataAccess.Repository.Primary.Address.Interface;
using ImageApi.DataAccess.Repository.Primary.ContactInfo.Interface;
using ImageApi.DataAccess.Repository.Primary.Document.Interface;
using ImageApi.DataAccess.Repository.Primary.DocumentDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.DocumentSignature.Interface;
using ImageApi.DataAccess.Repository.Primary.Login.Interface;
using ImageApi.DataAccess.Repository.Primary.LoginDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.RefreshToken.Interface;
using ImageApi.DataAccess.Repository.Primary.Share.Interface;
using ImageApi.DataAccess.Repository.Primary.ShareDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.SocialSecurityNumber.Interface;
using ImageApi.DataAccess.Repository.Primary.ValidationCode.Interface;

namespace ImageApi.DataAccess.UnitOfWork.Primary.Interface
{
    public interface IPrimaryUnitOfWork : IUnitOfWorkBase
    {
        IAccountRepository AccountRepository { get; }

        IAccountInfoRepository AccountInfoRepository { get; }

        IAccountRoleRepository AccountRoleRepository { get; }

        IAddressRepository AddressRepository { get; }

        IContactInfoRepository ContactInfoRepository { get; }

        IDocumentRepository DocumentRepository { get; }

        IDocumentDetailRepository DocumentDetailRepository { get; }

        IDocumentSignatureRepository DocumentSignatureRepository { get; }

        ILoginRepository LoginRepository { get; }

        ILoginDetailRepository LoginDetailRepository { get; }

        IRefreshTokenRepository RefreshTokenRepository { get; }

        IShareRepository ShareRepository { get; }

        IShareDetailRepository ShareDetailRepository { get; }

        ISocialSecurityNumberRepository SocialSecurityNumberRepository { get; }

        IValidationCodeRepository ValidationCodeRepository { get; }
    }
}