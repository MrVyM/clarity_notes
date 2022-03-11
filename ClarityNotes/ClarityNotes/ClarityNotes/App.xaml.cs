using System;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;

namespace ClarityNotes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (true)
                MainPage = new NavigationPage(new LoginPage());
            else
            {
                var page = new RootPage();
                NavigationPage.SetHasNavigationBar(page, false);
                MainPage = new NavigationPage(page);
            }

            string PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH + "/data");
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
