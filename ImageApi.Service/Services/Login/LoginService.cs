using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Login.Interface;
using ImageApi.Utilities;
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

        public async Task<string> ValidateRefreshToken(string oldRefreshToken, CancellationToken cancellationToken)
        {
            var refreshToken = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(oldRefreshToken, cancellationToken);
            if (refreshToken is null)
                return null;

            refreshToken.Token = RandomStringGenerator.Generate();
            refreshToken.Expiration = DateTimeOffset.UtcNow.AddDays(30);

            return refreshToken.Token;
        }
    }
}