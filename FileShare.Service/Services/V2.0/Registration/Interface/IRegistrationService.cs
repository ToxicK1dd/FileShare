using FileShare.Service.Dtos.V2._0.Registration;

namespace FileShare.Service.Services.V2._0.Registration.Interface
{
    public interface IRegistrationService
    {
        /// <summary>
        /// Create an account, and add it to the database.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="RegistrationResultDto"/> indicating whether or not the registration was successful. And a potential error message.</returns>
        Task<RegistrationResultDto> RegisterAsync(string username, string email, string password, CancellationToken cancellationToken);
    }
}