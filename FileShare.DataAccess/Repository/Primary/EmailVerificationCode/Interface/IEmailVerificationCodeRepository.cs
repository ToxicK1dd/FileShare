using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.EmailVerificationCode.EmailVerificationCode;

namespace FileShare.DataAccess.Repository.Primary.EmailVerificationCode.Interface
{
    public interface IEmailVerificationCodeRepository : IRepositoryBase<Model> { }
}