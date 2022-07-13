using ImageApi.DataAccess.Base.UnitOfWork;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Account;
using ImageApi.DataAccess.Repository.Primary.Account.Interface;
using ImageApi.DataAccess.Repository.Primary.AccountInfo;
using ImageApi.DataAccess.Repository.Primary.AccountInfo.Interface;
using ImageApi.DataAccess.Repository.Primary.Admin;
using ImageApi.DataAccess.Repository.Primary.Admin.Interface;
using ImageApi.DataAccess.Repository.Primary.Document;
using ImageApi.DataAccess.Repository.Primary.Document.Interface;
using ImageApi.DataAccess.Repository.Primary.DocumentDetail;
using ImageApi.DataAccess.Repository.Primary.DocumentDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.Login;
using ImageApi.DataAccess.Repository.Primary.Login.Interface;
using ImageApi.DataAccess.Repository.Primary.LoginDetail;
using ImageApi.DataAccess.Repository.Primary.LoginDetail.Interface;
using ImageApi.DataAccess.Repository.Primary.RefreshToken;
using ImageApi.DataAccess.Repository.Primary.RefreshToken.Interface;
using ImageApi.DataAccess.Repository.Primary.User;
using ImageApi.DataAccess.Repository.Primary.User.Interface;
using ImageApi.DataAccess.UnitOfWork.Primary.Interface;

namespace ImageApi.DataAccess.UnitOfWork.Primary
{
    public class PrimaryUnitOfWork : UnitOfWorkBase<PrimaryContext>, IPrimaryUnitOfWork
    {
        private AccountRepository accountRepository;
        private AccountInfoRepository accountInfoRepository;
        private AdminRepository adminRepository;
        private DocumentRepository documentRepository;
        private DocumentDetailRepository documentDetailRepository;
        private LoginRepository loginRepository;
        private LoginDetailRepository loginDetailRepository;
        private RefreshTokenRepository refreshTokenRepository;
        private UserRepository userRepository;

        public PrimaryUnitOfWork(PrimaryContext context) : base(context) { }


        public IAccountRepository AccountRepository { get => accountRepository ??= new(context); }

        public IAccountInfoRepository AccountInfoRepository { get => accountInfoRepository ??= new(context); }

        public IAdminRepository AdminRepository { get => adminRepository ??= new(context); }

        public IDocumentRepository DocumentRepository { get => documentRepository ??= new(context); }

        public IDocumentDetailRepository DocumentDetailRepository { get => documentDetailRepository ??= new(context); }

        public ILoginRepository LoginRepository { get => loginRepository ??= new(context); }

        public ILoginDetailRepository LoginDetailRepository { get => loginDetailRepository ??= new(context); }

        public IRefreshTokenRepository RefreshTokenRepository { get => refreshTokenRepository ??= new(context); }

        public IUserRepository UserRepository { get => userRepository ??= new(context); }
    }
}