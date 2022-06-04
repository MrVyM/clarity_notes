using System;
using Xamarin.Forms;
using QRCoder;
using System.IO;

namespace ClarityNotes
{
    public class QRCodeGeneratorPage : ContentPage
    {
        User user;
        Note note;

        public QRCodeGeneratorPage(User user, Note note)
        {
            this.user = user;
            this.note = note;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(user.Email, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(100);
            ImageSource QrCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));

            BackgroundImageSource = QrCodeImage;

        }
    }
}
