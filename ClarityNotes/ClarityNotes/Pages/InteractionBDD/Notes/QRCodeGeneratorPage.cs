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
        StackLayout stack;

        public QRCodeGeneratorPage(User user, Note note)
        {
            this.user = user;
            this.note = note;

            Button button = new Button
            {
                Text = "Générer un QR Code pour cette note",
                TextColor = Color.Black,
            };

            button.Clicked += ToQRCode;

            stack = new StackLayout();

            stack.Children.Add(button);

            Content = stack;
        }
        public void ToQRCode(object sender, EventArgs e)
        {
            stack.Children.Clear();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("coucou", QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);
            ImageSource QrCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));

            BackgroundImageSource = QrCodeImage;
        }
    }

    
}
