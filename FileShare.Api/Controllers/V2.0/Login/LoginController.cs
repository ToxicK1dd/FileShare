using FileShare.Api.Models.V2._0.Login;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.Login.Interface;
using FileShare.Service.Services.RefreshToken.Interface;
using FileShare.Service.Services.Token.Interface;
using FileShare.Service.Services.TotpMfa.Interface;
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
        private readonly ITotpMfaService _totpMfaService;
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public LoginController(
            IPrimaryUnitOfWork unitOfWork,
            ITotpMfaService totpMfaService,
            ILoginService loginService,
            ITokenService tokenService,
            IRefreshTokenService refreshTokenService)
        {
            _unitOfWork = unitOfWork;
            _totpMfaService = totpMfaService;
            _loginService = loginService;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }


        /// <summary>
        /// Obtain an access token, and refresh token using user credentials.
        /// </summary>
        /// <param name="model">User credentials.</param>
        /// <param name="isTemporary">If true, a refresh token will not be created.</param>
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
        /// <response code="201">A new access token, and refresh token has been created.</response>
        /// <response code="401">The credentials are incorrect.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateLoginModel model, [FromQuery] bool isTemporary = false)
        {
            var isTwoFactorEnabled = await _totpMfaService.IsTwoFactorEnabledAsync(model.Username);
            if (isTwoFactorEnabled)
                return Unauthorized("Two factor authentication is enabled for this account.");

            var isValidated = await _loginService.ValidateCredentialsByUsernameAsync(model.Username, model.Password);
            if (!isValidated)
                return Unauthorized("Password is incorrect.");

            var accessToken = await _tokenService.GetAccessTokenFromUsernameAsync(model.Username);
            var refreshToken = string.Empty;
            if (isTemporary is false)
                refreshToken = await _refreshTokenService.GetRefreshTokenFromUsernameAsync(model.Username);


            await _unitOfWork.SaveChangesAsync();

            return Created(string.Empty, new
            {
                accessToken,
                refreshToken
            });
        }

        /// <summary>
        /// Obtain an access token, and refresh token using user credentials, and TOTP code.
        /// </summary>
        /// <param name="model">User credentials.</param>
        /// <param name="isTemporary">If true, a refresh token will not be created.</param>
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
        /// <response code="201">A new access token, and refresh token has been created.</response>
        /// <response code="401">The credentials are incorrect.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AuthenticateTotp([FromBody] AuthenticateTotpLoginModel model, [FromQuery] bool isTemporary = false)
        {
            var isPasswordValidated = await _loginService.ValidateCredentialsByUsernameAsync(model.Username, model.Password);
            if (isPasswordValidated is false)
                return Unauthorized("Password is incorrect.");

            var isCodeValidated = await _loginService.ValidateTotpCodeAsync(model.Username, model.Code);
            if (isCodeValidated is false)
                return Unauthorized("2FA code is incorrect.");

            var accessToken = await _tokenService.GetAccessTokenFromUsernameAsync(model.Username);
            var refreshToken = string.Empty;
            if (isTemporary is false)
                refreshToken = await _refreshTokenService.GetRefreshTokenFromUsernameAsync(model.Username);

            await _unitOfWork.SaveChangesAsync();

            return Created(string.Empty, new
            {
                accessToken,
                refreshToken
            });
        }
    }
}