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
        /// <returns>Returns boolean indicating if the credentials are valid.</returns>
        Task<bool> ValidateCredentialsAsync(string username, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Change the password for the user.
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns boolean indicating a successful change of the password.</returns>
        Task<bool> ChangeCredentialsAsync(string newPassword, string oldPassword, CancellationToken cancellationToken);

        /// <summary>
        /// Ensure the refresh token are correct.
        /// </summary>
        /// <param name="oldRefreshToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an updated refresh token.</returns>
        Task<string> ValidateRefreshTokenAsync(string oldRefreshToken, CancellationToken cancellationToken);
    }
}