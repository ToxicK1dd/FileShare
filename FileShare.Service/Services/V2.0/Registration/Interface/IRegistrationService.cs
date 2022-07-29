namespace FileShare.Service.Services.V2._0.Registration.Interface
{
    public interface IRegistrationService
    {
        /// <summary>
        /// Create an account, and add it to the database.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns login, and account id for the newly created account.</returns>
        Task<(Guid loginId, Guid accountId)> RegisterAsync(string username, string email, string password, CancellationToken cancellationToken);
    }
}