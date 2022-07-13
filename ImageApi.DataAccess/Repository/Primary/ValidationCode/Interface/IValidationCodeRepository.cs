using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.ValidationCode.ValidationCode;

namespace ImageApi.DataAccess.Repository.Primary.ValidationCode.Interface
{
    public interface IValidationCodeRepository : IRepositoryBase<Model> { }
}