namespace ImageApi.Service.Services.Token.Interface
{
    public interface ITokenService
    {
        string GetAccessToken(Guid accountId);
        Task<string> GetRefreshTokenAsync(Guid accountId, CancellationToken cancellationToken);
    }
}