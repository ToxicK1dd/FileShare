using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Token.Interface;

namespace ImageApi.Service.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;

        public TokenService(IPrimaryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}