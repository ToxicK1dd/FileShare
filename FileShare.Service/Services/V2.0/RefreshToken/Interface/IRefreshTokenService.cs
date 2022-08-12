namespace FileShare.Service.Services.V2._0.RefreshToken.Interface
{
    public interface IRefreshTokenService
    {
        /// <summary>
        /// Ensure the refresh token are correct.
        /// </summary>
        /// <param name="oldRefreshToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An updated refresh token, or null if the old refresh token did not exist.</returns>
        Task<string> ValidateRefreshTokenAsync(string oldRefreshToken);
    }
}