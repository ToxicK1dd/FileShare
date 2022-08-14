using FileShare.DataAccess.Models.Primary.User;
using FileShare.Service.Dtos.TotpMfa;
using FileShare.Service.Services.TotpMfa.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FileShare.Service.Services.TotpMfa
{
    /// <summary>
    /// Methods for Time-Based One-Time Password (TOTP) Multi-Factor Authentication (MFA)
    /// </summary>
    public class TotpMfaService : ITotpMfaService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClaimsHelper _identityClaimsHelper;
        private readonly UserManager<User> _userManager;

        public TotpMfaService(
            IHttpContextAccessor httpContextAccessor,
            IIdentityClaimsHelper identityClaimsHelper,
            UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityClaimsHelper = identityClaimsHelper;
            _userManager = userManager;
        }


        public async Task<bool> EnableTwoFactorAsync()
        {
            var user = await GetCurrentUser();
            return await ToggleTwoFactor(user, true);
        }

        public async Task<bool> DisableTwoFactorAsync()
        {
            var user = await GetCurrentUser();
            return await ToggleTwoFactor(user, false);
        }

        public async Task<bool> IsTwoFactorEnabledAsync()
        {
            var user = await GetCurrentUser();
            return user.TwoFactorEnabled;
        }

        public async Task<bool> IsTwoFactorEnabledAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return false;

            return user.TwoFactorEnabled;
        }


        public async Task<TotpMfaCodesResultDto> GenerateTotpMfaKeyWithRecoveryCodesAsync()
        {
            var user = await GetCurrentUser();

            // Reset totp key
            var resetAuthResult = await _userManager.ResetAuthenticatorKeyAsync(user);
            if (resetAuthResult.Succeeded is false)
                return null;

            // Generate totp key
            var key = await _userManager.GetAuthenticatorKeyAsync(user);

            // Generate recovery codes
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            if (recoveryCodes.Any() is false)
                return null;

            return new(key, recoveryCodes);
        }

        public async Task<string> GenerateTotpMfaKeyFromRecoveryCodeAsync(string recoveryCode)
        {
            var user = await GetCurrentUser();

            // Validate recovery code
            var result = await _userManager.RedeemTwoFactorRecoveryCodeAsync(user, recoveryCode);
            if (result.Succeeded is false)
                return null;

            // Generate totp key
            var key = await _userManager.GetAuthenticatorKeyAsync(user);
            return key;
        }

        public async Task<bool> ResetTotpMfaKeyAsync(string code)
        {
            var user = await GetCurrentUser();

            // Validate totp code
            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Authenticator", code);
            if (result is false)
                return false;

            // Reset two factor authentication
            var resetAuthResult = await _userManager.ResetAuthenticatorKeyAsync(user);
            return resetAuthResult.Succeeded;
        }


        #region Helpers

        private async Task<bool> ToggleTwoFactor(User user, bool enabled)
        {
            // Toggle two factor authentication
            var enableTwoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user, enabled);
            return enableTwoFactorResult.Succeeded;
        }

        private async Task<User> GetCurrentUser()
        {
            var username = _identityClaimsHelper.GetUsernameFromHttpContext(_httpContextAccessor.HttpContext);
            return await _userManager.FindByNameAsync(username);
        }

        #endregion
    }
}