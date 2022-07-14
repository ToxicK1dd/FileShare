using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Login.Interface;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace ImageApi.Service.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginService(IPrimaryUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<bool> ValidateCredentials(string username, string password, CancellationToken cancellationToken)
        {
            var login = await _unitOfWork.LoginRepository.GetFromUsernameAsync(username, cancellationToken);

            var verificationResult = new PasswordHasher<object>().VerifyHashedPassword(null, login.Password, password);

            return verificationResult is PasswordVerificationResult.Success;
        }
    }
}