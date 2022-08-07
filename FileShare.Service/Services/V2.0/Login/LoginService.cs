using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Login.Interface;
using FileShare.Utilities.Generators.Random.Interface;
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
        private readonly UserManager<DataAccess.Models.Primary.User.User> _userManager;


        public LoginService(
            IHttpContextAccessor httpContextAccessor,
            IPrimaryUnitOfWork unitOfWork,
            IIdentityClaimsHelper identityClaimsHelper,
            IRandomGenerator randomGenerator,
            UserManager<DataAccess.Models.Primary.User.User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _identityClaimsHelper = identityClaimsHelper;
            _randomGenerator = randomGenerator;
            _userManager = userManager;
        }


        public async Task<bool> ValidateCredentialsByUsernameAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> ValidateCredentialsByEmailAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> ChangeCredentialsAsync(string newPassword, string oldPassword)
        {
            var username = _identityClaimsHelper.GetUsernameFromHttpContext(_httpContextAccessor.HttpContext);
            if (username is null)
                return false;

            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return false;

            var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);
            if (result is PasswordVerificationResult.Failed)
                return false;

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, newPassword);

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