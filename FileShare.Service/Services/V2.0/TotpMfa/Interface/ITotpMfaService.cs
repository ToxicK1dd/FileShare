using FileShare.Service.Dtos.V2._0.TotpMfa;

namespace FileShare.Service.Services.V2._0.TotpMfa.Interface
{
    public interface ITotpMfaService
    {
        /// <summary>
        /// Enable 2FA for the user.
        /// </summary>
        /// <returns><see langword="bool"/> indicating if 2FA was successfully enabled.</returns>
        Task<bool> EnableTwoFactor();

        /// <summary>
        /// Disable 2FA for the user.
        /// </summary>
        /// <returns><see langword="bool"/> indicating if 2FA was successfully disabled.</returns>
        Task<bool> DisableTwoFactor();

        /// <summary>
        /// Get if 2FA is currently enabled.
        /// </summary>
        /// <returns><see langword="bool"/> indicating if 2FA is currently enabled.</returns>
        Task<bool> IsTwoFactorEnabled();


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