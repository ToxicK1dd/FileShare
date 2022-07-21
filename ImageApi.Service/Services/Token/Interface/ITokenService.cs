namespace ImageApi.Service.Services.Token.Interface
{
    public interface ITokenService
    {
        /// <summary>
        /// Generate JWT for authenticating.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        string GetAccessToken(Guid accountId);

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
        /// <param name="accountId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetRefreshTokenAsync(Guid accountId, CancellationToken cancellationToken);

        /// <summary>
        /// Generate refresh token from username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetRefreshTokenFromUsernameAsync(string username, CancellationToken cancellationToken);
    }
}