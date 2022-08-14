using FileShare.Service.Dtos.Registration;

namespace FileShare.Service.Services.Registration.Interface
{
    public interface IRegistrationService
    {
        /// <summary>
        /// Create an account, and add it to the database.
        /// </summary>
        /// <returns><see cref="RegistrationResultDto"/> indicating whether or not the registration was successful. And a potential error message.</returns>
        Task<RegistrationResultDto> RegisterAsync(RegisterDto dto);
    }
}