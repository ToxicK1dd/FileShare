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
    }
}