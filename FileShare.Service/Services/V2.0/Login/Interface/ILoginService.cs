namespace FileShare.Service.Services.V2._0.Login.Interface
{
    public interface ILoginService
    {
        /// <summary>
        /// Ensure the username and password are correct.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see langword="true"/> if the credentials are valid. Otherwise <see langword="false"/>.</returns>
        Task<bool> ValidateCredentialsByUsernameAsync(string username, string password);

        /// <summary>
        /// Ensure the email and password are correct.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns><see langword="true"/> if the credentials are valid. Otherwise <see langword="false"/>.</returns>
        Task<bool> ValidateCredentialsByEmailAsync(string email, string password);

        /// <summary>
        /// Ensure the username, password, and TOTP code are correct.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> ValidateTotpCodeAsync(string username, string code);

        /// <summary>
        /// Change the password for the user.
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see langword="true"/> if the credentials were successfully changed. Otherwise <see langword="false"/>.</returns>
        Task<bool> ChangeCredentialsAsync(string newPassword, string oldPassword);
    }
}