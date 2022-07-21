namespace ImageApi.Service.Services.Login.Interface
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
        Task<bool> ValidateCredentials(string username, string password, CancellationToken cancellationToken);
        
        /// <summary>
        /// Ensure the refresh token are correct.
        /// </summary>
        /// <param name="oldRefreshToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an updated refresh token.</returns>
        Task<string> ValidateRefreshToken(string oldRefreshToken, CancellationToken cancellationToken);
    }
}