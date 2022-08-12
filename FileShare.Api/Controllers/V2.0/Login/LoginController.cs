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
    }
}