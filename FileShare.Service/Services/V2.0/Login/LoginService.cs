using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Login.Interface;
using FileShare.Utilities.Generators.Random;
using FileShare.Utilities.Generators.Random.Interface;
using FileShare.Utilities.Helpers.IdentityClaims;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
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
        private readonly IIdentityClaimsHelper _identityClaimsHelper;
        private readonly IRandomGenerator _randomGenerator;
        private readonly IPasswordHasher<object> _passwordHasher;

        public LoginService(
            IHttpContextAccessor httpContextAccessor,
            IPrimaryUnitOfWork unitOfWork,
            IIdentityClaimsHelper identityClaimsHelper,
            IRandomGenerator randomGenerator,
            IPasswordHasher<object> passwordHasher)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _identityClaimsHelper = identityClaimsHelper;
            _randomGenerator = randomGenerator;
            _passwordHasher = passwordHasher;
        }


        public async Task<bool> ValidateCredentialsAsync(string username, string password, CancellationToken cancellationToken)
        {
            var login = await _unitOfWork.LoginRepository.GetFromUsernameAsync(username, cancellationToken);
            if (login is null)
                return false;

            var verificationResult = _passwordHasher.VerifyHashedPassword(null, login.Password, password);

            return verificationResult is PasswordVerificationResult.Success;
        }

        public async Task<bool> ChangeCredentialsAsync(string newPassword, string oldPassword, CancellationToken cancellationToken)
        {
            var accountId = _identityClaimsHelper.GetAccountIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (accountId == Guid.Empty)
                return false;

            var login = await _unitOfWork.LoginRepository.GetByIdAsync(accountId, cancellationToken);
            if (login is null)
                return false;

            var verificationResult = _passwordHasher.VerifyHashedPassword(null, login.Password, oldPassword);
            if (verificationResult is not PasswordVerificationResult.Success)
                return false;

            login.Password = _passwordHasher.HashPassword(null, newPassword);
            return true;
        }

        public async Task<string> ValidateRefreshTokenAsync(string oldRefreshToken, CancellationToken cancellationToken)
        {
            var refreshToken = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(oldRefreshToken, cancellationToken);
            if (refreshToken is null)
                return null;

            refreshToken.Token = _randomGenerator.GenerateBase64String();
            refreshToken.Expiration = DateTimeOffset.UtcNow.AddDays(30);

            return refreshToken.Token;
        }
    }
}