using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.VerificationCode.VerificationCode;

namespace ImageApi.DataAccess.Repository.Primary.ValidationCode.Interface
{
    public interface IVerificationCodeRepository : IRepositoryBase<Model> { }
}