namespace FileShare.Service.Services.Token.Interface
{
    public interface ITokenService
    {
        /// <summary>
        /// Generate an access token for authenticating.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A new access containing user claims.</returns>
        Task<string> GetAccessTokenFromUserIdAsync(Guid userId);

        /// <summary>
        /// Generate an access token from username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A new access token containing user claims.</returns>
        Task<string> GetAccessTokenFromUsernameAsync(string username);

        /// <summary>
        /// Generate an access token from refresh token.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<string> GetAccessTokenFromRefreshToken(string refreshToken);

        /// <summary>
        /// Generate refresh token for authenticating.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A new refresh token.</returns>
        Task<string> GetRefreshTokenFromUserIdAsync(Guid userId);

        /// <summary>
        /// Generate refresh token from username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A new refresh token.</returns>
        Task<string> GetRefreshTokenFromUsernameAsync(string username);
    }
}