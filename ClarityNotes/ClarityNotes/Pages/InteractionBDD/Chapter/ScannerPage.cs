using Xamarin.Forms;
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

        public void GoToRootPage()
        {
            var page = new RootPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
        }
    }
}
