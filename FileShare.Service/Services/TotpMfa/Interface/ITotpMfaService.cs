using FileShare.Service.Dtos.TotpMfa;

namespace FileShare.Service.Services.TotpMfa.Interface
{
    public interface ITotpMfaService
    {
        /// <summary>
        /// Enable 2FA for the user.
        /// </summary>
        /// <returns><see langword="bool"/> indicating if 2FA was successfully enabled.</returns>
        Task<bool> EnableTwoFactorAsync();

        /// <summary>
        /// Disable 2FA for the user.
        /// </summary>
        /// <returns><see langword="bool"/> indicating if 2FA was successfully disabled.</returns>
        Task<bool> DisableTwoFactorAsync();

        /// <summary>
        /// Get if 2FA is enabled for the current authenticated user.
        /// </summary>
        /// <returns><see langword="bool"/> indicating if 2FA is currently enabled.</returns>
        Task<bool> IsTwoFactorEnabledAsync();

        /// <summary>
        /// Get if 2FA is enabled from username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns><see langword="bool"/> indicating if 2FA is currently enabled.</returns>
        Task<bool> IsTwoFactorEnabledAsync(string username);


        /// <summary>
        /// Generates TOTP MFA key, and recovery codes for the user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>The key for generating TOTP codes.</returns>
        Task<TotpMfaCodesResultDto> GenerateTotpMfaKeyWithRecoveryCodesAsync();

        /// <summary>
        /// Generates the TOTP MFA key from a recovery code.
        /// </summary>
        /// <param name="recoveryCode"></param>
        /// <returns>The key for generating TOTP codes. Otherwise <see langword="null"/> if the code is invalid.</returns>
        Task<string> GenerateTotpMfaKeyFromRecoveryCodeAsync(string recoveryCode);

        /// <summary>
        /// Resets the TOTP MFA key for the user.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> ResetTotpMfaKeyAsync(string code);
    }
}