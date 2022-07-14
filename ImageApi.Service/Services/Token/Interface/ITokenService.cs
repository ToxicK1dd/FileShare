namespace ImageApi.Service.Services.Token.Interface
{
    public interface ITokenService
    {
        string GetAccessToken(Guid accountId);

        Task<string> GetAccessTokenFromUsernameAsync(string username, CancellationToken cancellationToken);

        Task<string> GetRefreshTokenAsync(Guid accountId, CancellationToken cancellationToken);

        Task<string> GetRefreshTokenFromUsernameAsync(string username, CancellationToken cancellationToken);
    }
}