namespace FileShare.Service.Services.V2._0.Token.Interface
{
    public interface ITokenService
    {
        /// <summary>
        /// Generate JWT for authenticating.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A signed JWT containing user claims.</returns>
        Task<string> GetAccessTokenFromUserIdAsync(Guid userId);

        /// <summary>
        /// Generate JWT from username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A signed JWT containing user claims.</returns>
        Task<string> GetAccessTokenFromUsernameAsync(string username, CancellationToken cancellationToken);

        /// <summary>
        /// Generate refresh tokem for authenticating.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A new refresh token.</returns>
        Task<string> GetRefreshTokenFromUserIdAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Generate refresh token from username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A new refresh token.</returns>
        Task<string> GetRefreshTokenFromUsernameAsync(string username, CancellationToken cancellationToken);
    }
}