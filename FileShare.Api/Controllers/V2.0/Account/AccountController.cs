using FileShare.Service.Services.V2._0.Account.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Controllers.V2._0.Account
{
    [ApiVersion("2.0")]
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Enable Time-Based One-Time Password (TOTP) Multi-Factor Authentication (MFA)
        /// </summary>
        /// <response code="200">TOTP MFA was successfully enabled.</response>
        [HttpPost]
        [ActionName("TOTP")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> EnableTotpMfa()
        {
            var key = await _accountService.EnableTotpMfaAsync();
            return Ok(key);
        }

        /// <summary>
        /// Disable Time-Based One-Time Password (TOTP) Multi-Factor Authentication (MFA)
        /// </summary>
        /// <response code="200">TOTP MFA was successfully disabled.</response>
        [HttpDelete]
        [ActionName("TOTP")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DisableTotpMfa()
        {
            var result = await _accountService.DisableTotpMfaAsync();
            return Ok(result);
        }
    }
}