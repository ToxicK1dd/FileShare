using ImageApi.DataAccess.Base.UnitOfWork.Interface;
using ImageApi.DataAccess.Repository.Primary.Account.Interface;
using ImageApi.DataAccess.Repository.Primary.AccountInfo.Interface;
using ImageApi.DataAccess.Repository.Primary.Admin.Interface;
using ImageApi.DataAccess.Repository.Primary.Document.Interface;
using ImageApi.DataAccess.Repository.Primary.DocumentDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.Login.Interface;
using ImageApi.DataAccess.Repository.Primary.LoginDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.RefreshToken.Interface;
using ImageApi.DataAccess.Repository.Primary.User.Interface;

namespace ImageApi.DataAccess.UnitOfWork.Primary.Interface
{
    public interface IPrimaryUnitOfWork : IUnitOfWorkBase
    {
        IAccountRepository AccountRepository { get; }

        IAccountInfoRepository AccountInfoRepository { get; }

        IAdminRepository AdminRepository { get; }

        IDocumentRepository DocumentRepository { get; }

        IDocumentDetailRepository DocumentDetailRepository { get; }

        ILoginRepository LoginRepository { get; }

        ILoginDetailRepository LoginDetailRepository { get; }

        IRefreshTokenRepository RefreshTokenRepository { get; }

        IUserRepository UserRepository { get; }
    }
}