namespace ImageApi.Service.Services.Login.Interface
{
    public interface ILoginService
    {
        Task<bool> ValidateCredentials(string username, string password, CancellationToken cancellationToken);
    }
}