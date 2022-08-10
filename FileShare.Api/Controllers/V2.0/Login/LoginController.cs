using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Api.Models.V2._0.Login;
using FileShare.Service.Services.V2._0.Login.Interface;
using FileShare.Service.Services.V2._0.Token.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Controllers.V2._0.Login
{
    /// <summary>
    /// Endpoints for authenticating users.
    /// </summary>
    [ApiVersion("2.0")]
    public class LoginController : BaseController
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;

        public LoginController(
            IPrimaryUnitOfWork unitOfWork,
            ILoginService loginService,
            ITokenService tokenService)
        {
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
            var isValidated = await _loginService.ValidateCredentialsByUsernameAsync(model.Username, model.Password);
            if (!isValidated)
                return Unauthorized("Password is incorrect.");

            var token = await _tokenService.GetAccessTokenFromUsernameAsync(model.Username);
            var refreshToken = await _tokenService.GetRefreshTokenFromUsernameAsync(model.Username);

            await _unitOfWork.SaveChangesAsync();

            return Created(string.Empty, new
            {
                token,
                refreshToken
            });
        }

        /// <summary>
        /// Get a new JWT, and refresh token using user credentials and TOTP code.
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /authenticatetotp
        ///     {
        ///        "username": "Superman",
        ///        "password": "!Krypton1t3",
        ///        "code": "261978"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">A new JWt, and refresh token has been created.</response>
        /// <response code="401">The credentials are incorrect.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AuthenticateTotp([FromBody] AuthenticateTotpLoginModel model)
        {
            var isPasswordValidated = await _loginService.ValidateCredentialsByUsernameAsync(model.Username, model.Password);
            if (isPasswordValidated is false)
                return Unauthorized("Password is incorrect.");

            var isCodeValidated = await _loginService.ValidateTotpCodeAsync(model.Username, model.Code);
            if (isCodeValidated is false)
                return Unauthorized("2FA code is incorrect.");

            var token = await _tokenService.GetAccessTokenFromUsernameAsync(model.Username);
            var refreshToken = await _tokenService.GetRefreshTokenFromUsernameAsync(model.Username);

            await _unitOfWork.SaveChangesAsync();

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
            var token = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(refreshToken);
            if (token is null)
                return NotFound("Refresh token could not be found.");

            _unitOfWork.RefreshTokenRepository.Remove(token);
            await _unitOfWork.SaveChangesAsync();

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
            var newRefreshToken = await _loginService.ValidateRefreshTokenAsync(refreshToken);
            if (newRefreshToken is null)
                return NotFound("Refresh token is invalid.");

            var userId = await _unitOfWork.RefreshTokenRepository.GetUserIdFromToken(refreshToken);
            var token = _tokenService.GetAccessTokenFromUserIdAsync(userId);

            await _unitOfWork.SaveChangesAsync();

            return Created(string.Empty, new
            {
                token,
                refreshToken = newRefreshToken
            });
        }
    }
}