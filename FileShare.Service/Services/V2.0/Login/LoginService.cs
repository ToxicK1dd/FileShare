using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Login.Interface;
using FileShare.Utilities.Generators;
using FileShare.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FileShare.Service.Services.V2._0.Login
{
    /// <summary>
    /// Methods for authentication, and password management.
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPrimaryUnitOfWork _unitOfWork;

        public LoginService(IHttpContextAccessor httpContextAccessor, IPrimaryUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> ValidateCredentials(string username, string password, CancellationToken cancellationToken)
        {
            var login = await _unitOfWork.LoginRepository.GetFromUsernameAsync(username, cancellationToken);
            if (login is null)
                return false;

            var verificationResult = new PasswordHasher<object>().VerifyHashedPassword(null, login.Password, password);

            return verificationResult is PasswordVerificationResult.Success;
        }

        public async Task<bool> ChangeCredentials(string newPassword, string oldPassword, CancellationToken cancellationToken)
        {
            var accountId = _httpContextAccessor.HttpContext.GetAccountIdFromHttpContext();
            if (accountId == Guid.Empty)
                return false;

            var login = await _unitOfWork.LoginRepository.GetByIdAsync(accountId, cancellationToken);
            if (login is null)
                return false;

            var verificationResult = new PasswordHasher<object>().VerifyHashedPassword(null, login.Password, oldPassword);
            if (verificationResult is not PasswordVerificationResult.Success)
                return false;

            login.Password = new PasswordHasher<object>().HashPassword(null, newPassword);
            return true;
        }

        public async Task<string> ValidateRefreshToken(string oldRefreshToken, CancellationToken cancellationToken)
        {
            var refreshToken = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(oldRefreshToken, cancellationToken);
            if (refreshToken is null)
                return null;

            refreshToken.Token = RandomGenerator.GenerateBase64String();
            refreshToken.Expiration = DateTimeOffset.UtcNow.AddDays(30);

            return refreshToken.Token;
        }
    }
}