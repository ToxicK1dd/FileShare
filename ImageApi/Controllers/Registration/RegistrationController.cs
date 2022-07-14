using ImageApi.Controllers.Share;
using ImageApi.Service.Services.Registration.Interface;

namespace ImageApi.Controllers.Registration
{
    public class RegistrationController : BaseController
    {
        private readonly ILogger<ShareController> _logger;
        private readonly IRegistrationService _registrationService;

        public RegistrationController(ILogger<ShareController> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }
    }
}