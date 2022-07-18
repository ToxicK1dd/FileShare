using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.EmailVerificationCode.EmailVerificationCode;

namespace ImageApi.DataAccess.Repository.Primary.EmailVerificationCode.Interface
{
    public interface IEmailVerificationCodeRepository : IRepositoryBase<Model> { }
}