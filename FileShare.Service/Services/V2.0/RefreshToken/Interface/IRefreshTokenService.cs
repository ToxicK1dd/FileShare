namespace FileShare.Service.Services.V2._0.RefreshToken.Interface
{
    public interface IRefreshTokenService
    {
        /// <summary>
        /// Ensure the refresh token is valid.
        /// </summary>
        /// <param name="oldRefreshToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An updated refresh token, or null if the old refresh token did not exist.</returns>
        Task<bool> ValidateRefreshTokenAsync(string oldRefreshToken);

        /// <summary>
        /// Rotate the refresh token, and extend expiration date.
        /// </summary>
        /// <param name="oldRefreshToken"></param>
        /// <returns></returns>
        Task<string> RotateRefreshTokenAsync(string oldRefreshToken);

        /// <summary>
        /// Revoke the access token, to prevent generation of new access tokens.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns><see langword="true"/> if the refresh token was found, and revoked. <see langword="false"/> if the refresh token was not found.</returns>
        Task<bool> RevokeRefreshTokenAsync(string refreshToken);
    }
}