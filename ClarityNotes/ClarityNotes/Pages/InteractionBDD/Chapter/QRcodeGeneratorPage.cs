using System;
using Xamarin.Forms;
using QRCoder;
using System.IO;

namespace ClarityNotes
{
    public class QRCodeGeneratorPage : ContentPage
    {
        User user;
        int idDirectory;

        public QRCodeGeneratorPage(User user,int idDirectory)
        {
            this.user = user;
            this.idDirectory = idDirectory;

            string QRcontent = user.Username + "/" + idDirectory;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(QRcontent, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(100);
            ImageSource QrCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
            
            StackLayout stack = new StackLayout();
   
            BackgroundImageSource = QrCodeImage;
        }
    }
}
