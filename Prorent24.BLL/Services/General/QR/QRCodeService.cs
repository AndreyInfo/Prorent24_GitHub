using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Prorent24.BLL.Services.General.QR
{
    internal class QRCodeService: IQRCodeService
    {
        public Bitmap getCode(string key) {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(key, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return qrCodeImage;
        }
    }
}
