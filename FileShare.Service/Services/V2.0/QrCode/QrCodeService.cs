﻿using FileShare.Service.Services.V2._0.QrCode.Interface;
using QRCoder;

namespace FileShare.Service.Services.V2._0.QrCode
{
    public class QrCodeService : IQrCodeService
    {
        public string GenerateQrCode(string text, int pixelsPerModule = 10)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            BitmapByteQRCode qrCode = new(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(pixelsPerModule);

            return Convert.ToBase64String(qrCodeBytes);
        }
    }
}