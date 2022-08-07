namespace FileShare.Service.Services.V2._0.Account.Interface
{
    public interface IAccountService
    {
        /// <summary>
        /// Enables Time-Based One-Time Password (TOTP) Multi-Factor Authentication (MFA) for the user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>The key for generating TOTP codes.</returns>
        Task<string> EnableTotpMfaAsync();

        /// <summary>
        /// Disables Time-Based One-Time Password (TOTP) Multi-Factor Authentication (MFA) for the user.
        /// </summary>
        /// <returns><see langword="bool"/> indicating whether or not the MFA was disabled.</returns>
        Task<bool> DisableTotpMfaAsync();
    }
}