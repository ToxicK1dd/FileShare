using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Api.Models.V2._0.Registration;
using FileShare.Service.Services.V2._0.Registration.Interface;
using FileShare.Service.Services.V2._0.Token.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Controllers.V2._0.Registration
{
    /// <summary>
    /// Endpoints for registering users.
    /// </summary>
    [ApiVersion("2.0")]
    public class RegistrationController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
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
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _registrationService = registrationService;
            _tokenService = tokenService;
        }


        /// <summary>
        /// Registers a new account.
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /register
        ///     {
        ///        "username": "Superman",
        ///        "email": "superman@kryptonmail.space",
        ///        "password": "!Krypton1t3"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns a newly created JWt, and refresh token.</response>
        /// <response code="500">The username, or email is already in use.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            var result = await _registrationService.RegisterAsync(model.Username, model.Email, model.Password, _httpContextAccessor.HttpContext.RequestAborted);

            var token = _tokenService.GetAccessToken(result.UserId);
            var refreshToken = await _tokenService.GetRefreshTokenAsync(result.LoginId, _httpContextAccessor.HttpContext.RequestAborted);

            await _unitOfWork.SaveChangesAsync(_httpContextAccessor.HttpContext.RequestAborted);

            return Created(string.Empty, new
            {
                token,
                refreshToken
            });
        }
    }
}