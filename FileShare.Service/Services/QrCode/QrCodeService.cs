using FileShare.Service.Services.QrCode.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Http;
using QRCoder;

namespace FileShare.Service.Services.QrCode
{
    public class QrCodeService : IQrCodeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClaimsHelper _identityClaimsHelper;

        public QrCodeService(
            IHttpContextAccessor httpContextAccessor,
            IIdentityClaimsHelper identityClaimsHelper)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityClaimsHelper = identityClaimsHelper;
        }


        public string GenerateTotpMfaQrCode(string key)
        {
            var username = _identityClaimsHelper.GetUsernameFromHttpContext(_httpContextAccessor.HttpContext);
            return GenerateQrCode($"otpauth://totp/FileShare ({username})?secret={key}");
        }


        #region Helpers

        private static string GenerateQrCode(string text, int pixelsPerModule = 10)
        {
            using QRCodeGenerator qrGenerator = new();
            using QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            using BitmapByteQRCode qrCode = new(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(pixelsPerModule);

            return Convert.ToBase64String(qrCodeBytes);
        }

        #endregion
    }
}