namespace FileShare.Service.Services.V2._0.QrCode.Interface
{
    public interface IQrCodeService
    {
        /// <summary>
        /// Generate a QR code from text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pixelsPerModule"></param>
        /// <returns>Base64 QR code.</returns>
        string GenerateQrCode(string text, int pixelsPerModule = 10);
    }
}