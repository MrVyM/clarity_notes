using System;
using Xamarin.Forms;
using QRCoder;
using System.IO;
using ZXing.Net.Mobile.Forms;

namespace ClarityNotes
{
    public class ScannerPage : ZXingScannerPage
    {
        User user;
        public ScannerPage(User user)
        {
            this.user = user;            
        }
    }
}
