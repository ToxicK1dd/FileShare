using FileShare.Service.Dtos.RefreshToken;

namespace FileShare.Service.Services.RefreshToken.Interface
{
    public interface IRefreshTokenService
    {
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

        /// <summary>
        /// Revoke the access token, to prevent generation of new access tokens.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns><see langword="true"/> if the refresh token was found, and revoked. <see langword="false"/> if the refresh token was not found.</returns>
        Task<bool> RevokeRefreshTokenFromIdAsync(Guid id);

        /// <summary>
        /// Delete refresh token from the database by issued refresh token string.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<bool> DeleteRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Delete refresh token from the database by primary id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteRefreshTokenFromIdAsync(Guid id);

        /// <summary>
        /// Get refresh tokens related to the user.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns>List of tokens.</returns>
        Task<IEnumerable<RefreshTokenDto>> GetRefreshTokensAsync(int page, int size);
    }
}