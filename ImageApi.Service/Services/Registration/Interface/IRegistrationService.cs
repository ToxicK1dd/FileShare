using ImageApi.Service.Dto.Registration;

namespace ImageApi.Service.Services.Registration.Interface
{
    public interface IRegistrationService
    {
        Task<(Guid loginId, Guid accountId)> Register(RegistrationDto dto, CancellationToken cancellationToken);
    }
}