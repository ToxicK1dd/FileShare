using FileShare.Service.Services.TotpMfa.Interface;
using FileShare.Service.Services.QrCode.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Controllers.V2._0.TotpMfa
{
    /// <summary>
    /// Manage Time-Based One-Time Password (TOTP) Multi-Factor Authentication (MFA)
    /// </summary>
    [ApiVersion("2.0")]
    public class TotpMfaController : BaseController
    {
        private readonly ITotpMfaService _totpMfaService;
        private readonly IQrCodeService _qrCodeService;

        public TotpMfaController(ITotpMfaService totpMfaService, IQrCodeService qrCodeService)
        {
            _totpMfaService = totpMfaService;
            _qrCodeService = qrCodeService;
        }


        /// <summary>
        /// Enable TOTP MFA
        /// </summary>
        /// <response code="200">TOTP MFA was successfully enabled.</response>
        [HttpGet]
        [ActionName("Enable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EnableTotpMfa()
        {
            var isEnabled = await _totpMfaService.IsTwoFactorEnabledAsync();
            if (isEnabled)
                return BadRequest("2FA is already enabled.");

            var isSuccessful = await _totpMfaService.EnableTwoFactorAsync();
            if (isSuccessful is false)
                return BadRequest("Failed to enable 2FA");

            var dto = await _totpMfaService.GenerateTotpMfaKeyWithRecoveryCodesAsync();
            if (dto is null)
                return Problem("Problem occured while generating keys.", statusCode: StatusCodes.Status500InternalServerError);

            var qrCode = _qrCodeService.GenerateTotpMfaQrCode(dto.Key);

            return Ok(new
            {
                recoveryCodes = dto.RecoveryCodes,
                qrCode
            });
        }

        /// <summary>
        /// Disable TOTP MFA
        /// </summary>
        /// <response code="200">TOTP MFA was successfully disabled.</response>
        [HttpGet]
        [ActionName("Disable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DisableTotpMfa([FromQuery] string authenticationCode)
        {
            var isEnabled = await _totpMfaService.IsTwoFactorEnabledAsync();
            if (isEnabled is false)
                return BadRequest("2FA is already disabled.");

            var isReset = await _totpMfaService.ResetTotpMfaKeyAsync(authenticationCode);
            if (isReset is false)
                return BadRequest("Invalid code authentication code.");

            var isSuccessful = await _totpMfaService.DisableTwoFactorAsync();
            if (isSuccessful is false)
                return BadRequest("Failed to disable 2FA");

            return Ok();
        }

        /// <summary>
        /// Recover TOTP MFA key
        /// </summary>
        /// <param name="recoveryCode"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Recover")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RecoverTotpMfa([FromQuery] string recoveryCode)
        {
            var key = await _totpMfaService.GenerateTotpMfaKeyFromRecoveryCodeAsync(recoveryCode);
            if (key is null)
                return BadRequest("Invalid recovery code");

            var qrCode = _qrCodeService.GenerateTotpMfaQrCode(key);

            return Ok(new
            {
                qrCode
            });
        }
    }
}