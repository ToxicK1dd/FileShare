using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.Login.Interface;
using FileShare.Utilities.Generators.Random.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FileShare.Service.Services.Login
{
    /// <summary>
    /// Methods for authentication, and password management.
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClaimsHelper _identityClaimsHelper;
        private readonly UserManager<DataAccess.Models.Primary.User.User> _userManager;


        public LoginService(
            IHttpContextAccessor httpContextAccessor,
            IIdentityClaimsHelper identityClaimsHelper,
            UserManager<DataAccess.Models.Primary.User.User> userManager)
        {
            _httpContextAccessor = httpContextAccessor; ;
            _identityClaimsHelper = identityClaimsHelper;
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

        public async Task<bool> ValidateTotpCodeAsync(string username, string code)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return false;

            return await _userManager.VerifyTwoFactorTokenAsync(user, "Authenticator", code);
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
    }
}