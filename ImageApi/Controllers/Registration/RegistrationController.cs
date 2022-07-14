using ImageApi.Service.Services.Registration.Interface;

namespace ImageApi.Controllers.Registration
{
    public class RegistrationController : BaseController
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IRegistrationService _registrationService;

        public RegistrationController(ILogger<RegistrationController> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }
    }
}