namespace FileShare.Service.Services.V2._0.Token.Interface
{
    public interface ITokenService
    {
        /// <summary>
        /// Generate JWT for authenticating.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetAccessToken(Guid userId);

        /// <summary>
        /// Generate JWT from username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetAccessTokenFromUsernameAsync(string username, CancellationToken cancellationToken);

        /// <summary>
        /// Generate refresh tokem for authenticating.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetRefreshTokenAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Generate refresh token from username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetRefreshTokenFromUsernameAsync(string username, CancellationToken cancellationToken);
    }
}