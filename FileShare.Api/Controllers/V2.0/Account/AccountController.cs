using FileShare.Service.Services.V2._0.Account.Interface;
using FileShare.Service.Services.V2._0.QrCode.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Controllers.V2._0.Account
{
    [ApiVersion("2.0")]
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IQrCodeService _qrCodeService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IQrCodeService qrCodeService)
        {
            _logger = logger;
            _accountService = accountService;
            _qrCodeService = qrCodeService;
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

            var qrCode = _qrCodeService.GenerateQrCode($"otpauth://totp/FileShare?secret={key}");

            return Ok(qrCode);
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