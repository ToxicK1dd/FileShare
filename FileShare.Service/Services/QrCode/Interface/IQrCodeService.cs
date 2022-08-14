namespace FileShare.Service.Services.QrCode.Interface
{
    public interface IQrCodeService
    {
        /// <summary>
        /// Generate a QR code for TOTP MFA.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pixelsPerModule"></param>
        /// <returns>Base64 QR code.</returns>
        string GenerateTotpMfaQrCode(string key);
    }
}