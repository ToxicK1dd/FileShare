using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Dto.Login;
using ImageApi.Service.Services.Login.Interface;
using ImageApi.Service.Services.Token.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers.V2._0.Login
{
    [ApiVersion("2.0")]
    public class LoginController : BaseController
    {
        private readonly HttpContext _httpContext;
        private readonly ILogger<LoginController> _logger;
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;

        public LoginController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<LoginController> logger,
            IPrimaryUnitOfWork unitOfWork,
            ILoginService loginService,
            ITokenService tokenService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _loginService = loginService;
            _tokenService = tokenService;
        }


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateLoginDto dto)
        {
            var isValidated = await _loginService.ValidateCredentials(dto.Username, dto.Password, _httpContext.RequestAborted);
            if (!isValidated)
                return Unauthorized();

            var token = await _tokenService.GetAccessTokenFromUsernameAsync(dto.Username, _httpContext.RequestAborted);
            var refreshToken = await _tokenService.GetRefreshTokenFromUsernameAsync(dto.Username, _httpContext.RequestAborted);

            await _unitOfWork.SaveChangesAsync(_httpContext.RequestAborted);

            return Created(string.Empty, new
            {
                token,
                refreshToken
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromQuery] string refreshToken)
        {
            return Created(string.Empty, "12345678");
        }
    }
}