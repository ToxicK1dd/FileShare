using FileShare.Service.Services.V2._0.Account.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FileShare.Service.Services.V2._0.Account
{
    /// <summary>
    /// Methods for managing accounts.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClaimsHelper _identityClaimsHelper;
        private readonly UserManager<DataAccess.Models.Primary.User.User> _userManager;

        public AccountService(
            IHttpContextAccessor httpContextAccessor,
            IIdentityClaimsHelper identityClaimsHelper,
            UserManager<DataAccess.Models.Primary.User.User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityClaimsHelper = identityClaimsHelper;
            _userManager = userManager;
        }


        public async Task<string> EnableTotpMfaAsync()
        {
            var username = _identityClaimsHelper.GetUsernameFromHttpContext(_httpContextAccessor.HttpContext);
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return null;

            var key = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(key))
            {
                var resetAuthResult = await _userManager.ResetAuthenticatorKeyAsync(user);
                if (resetAuthResult.Succeeded is false)
                    return null;

                key = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            var enableTwoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user, true);
            if (enableTwoFactorResult.Succeeded is false)
                return null;

            return key;
        }

        public async Task<bool> DisableTotpMfaAsync()
        {
            var username = _identityClaimsHelper.GetUsernameFromHttpContext(_httpContextAccessor.HttpContext);
            if (username is null)
                return false;

            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return false;

            var resetKeyResult = await _userManager.ResetAuthenticatorKeyAsync(user);
            if (resetKeyResult.Succeeded is false)
                return false;

            var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
            return result.Succeeded;
        }
    }
}