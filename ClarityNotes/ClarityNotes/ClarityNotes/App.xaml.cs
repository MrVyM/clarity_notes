using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClarityNotes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (false)
                MainPage = new NavigationPage(new LoginPage());
            else
            {
                var page = new RootPage();
                NavigationPage.SetHasNavigationBar(page, false);
                MainPage = new NavigationPage(page);
            }
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
