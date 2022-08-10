using FileShare.Service.Services.V2._0.Password.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FileShare.Service.Services.V2._0.Password
{
    public class PasswordService : IPasswordService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClaimsHelper _identityClaimsHelper;
        private readonly UserManager<DataAccess.Models.Primary.User.User> _userManager;

        public PasswordService(
            IHttpContextAccessor httpContextAccessor,
            IIdentityClaimsHelper identityClaimsHelper,
            UserManager<DataAccess.Models.Primary.User.User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityClaimsHelper = identityClaimsHelper;
            _userManager = userManager;
        }


        public async Task<bool> ChangePasswordAsync(string oldPassword, string newPassword)
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

        public async Task<bool> ConfirmResetPasswordAsync(string email, string password, string confirmPassword, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return false;

            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (result.Succeeded is false)
                return false;

            return true;
        }

        public async Task<string> RequestResetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return null;

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            return code;
        }
    }
}