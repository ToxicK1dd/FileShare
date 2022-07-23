using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Models.V2._0.Login;
using ImageApi.Service.Services.Login.Interface;
using ImageApi.Service.Services.Token.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers.V2._0.Login
{
    /// <summary>
    /// Endpoints for authenticating users.
    /// </summary>
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


        /// <summary>
        /// Get a new JWT, and refresh token using user credentials.
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /authenticate
        ///     {
        ///        "username": "Superman",
        ///        "password": "!Krypton1t3"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">A new JWt, and refresh token has been created.</response>
        /// <response code="401">The credentials are incorrect.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateLoginModel model)
        {
            var isValidated = await _loginService.ValidateCredentials(model.Username, model.Password, _httpContext.RequestAborted);
            if (!isValidated)
                return Unauthorized();

            var token = await _tokenService.GetAccessTokenFromUsernameAsync(model.Username, _httpContext.RequestAborted);
            var refreshToken = await _tokenService.GetRefreshTokenFromUsernameAsync(model.Username, _httpContext.RequestAborted);

            await _unitOfWork.SaveChangesAsync(_httpContext.RequestAborted);

            return Created(string.Empty, new
            {
                token,
                refreshToken
            });
        }

        /// <summary>
        /// Remove the specified refresh token, to prevent the user from optaining a new JWT.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <response code="204">The refresh token has been deleted.</response>
        /// <response code="404">The specified refresh token could not be found.</response>
        [HttpDelete]
        [AllowAnonymous]
        [ActionName("RefreshToken")]
        public async Task<IActionResult> DeleteRefreshToken([FromQuery] string refreshToken)
        {
            var token = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(refreshToken, _httpContext.RequestAborted);
            if (token is null)
                return NotFound();

            _unitOfWork.RefreshTokenRepository.Remove(token);
            await _unitOfWork.SaveChangesAsync(_httpContext.RequestAborted);

            return NoContent();
        }

        /// <summary>
        /// Get a new JWT, using the refresh token. This will also replace the refresh token with a new one.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <response code="201">A new JWt, and refresh token has been created.</response>
        /// <response code="404">The specified refresh token could not be found.</response>
        [HttpPut]
        [AllowAnonymous]
        [ActionName("RefreshToken")]
        public async Task<IActionResult> UpdateRefreshToken([FromQuery] string refreshToken)
        {
            var newRefreshToken = await _loginService.ValidateRefreshToken(refreshToken, _httpContext.RequestAborted);
            if (newRefreshToken is null)
                return NotFound();

            var accountId = await _unitOfWork.RefreshTokenRepository.GetAccountIdFromToken(refreshToken, _httpContext.RequestAborted);
            var token = _tokenService.GetAccessToken(accountId);

            await _unitOfWork.SaveChangesAsync(_httpContext.RequestAborted);

            return Created(string.Empty, new
            {
                token,
                newRefreshToken
            });
        }

        /// <summary>
        /// Change the current password of the user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeCredentials([FromBody] ChangePasswordModel model)
        {
            await _loginService.ChangeCredentials(model.NewPassword, model.OldPassword, _httpContext.RequestAborted);
            await _unitOfWork.SaveChangesAsync(_httpContext.RequestAborted);

            return NoContent();
        }
    }
}