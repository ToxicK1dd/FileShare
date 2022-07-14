using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Dto.Registration;
using ImageApi.Service.Services.Registration.Interface;
using ImageApi.Service.Services.Token.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers.V2._0.Registration
{
    [ApiVersion("2.0")]
    public class RegistrationController : BaseController
    {
        private readonly HttpContext _httpContext;
        private readonly ILogger<RegistrationController> _logger;
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IRegistrationService _registrationService;
        private readonly ITokenService _tokenService;

        public RegistrationController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<RegistrationController> logger,
            IPrimaryUnitOfWork unitOfWork,
            IRegistrationService registrationService,
            ITokenService tokenService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _registrationService = registrationService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registers a new account.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegistrationDto dto)
        {
            var (loginId, accountId) = await _registrationService.Register(dto, _httpContext.RequestAborted);

            var token = _tokenService.GetAccessToken(accountId);
            var refreshToken = await _tokenService.GetRefreshTokenAsync(loginId, _httpContext.RequestAborted);

            await _unitOfWork.SaveChangesAsync(_httpContext.RequestAborted);

            return Created(string.Empty, new
            {
                token,
                refreshToken
            });
        }
    }
}