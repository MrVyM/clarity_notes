using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClarityNotes
{
    public partial class App : Application
    {
        public App()
        {
            
            try
            {
                InitializeComponent();
                MainPage = new NavigationPage(new ErrorServerPage("Serveur Introuvable\nVeuillez vous assurer que votre appareil est connecté au réseau."));
            } catch
            {
                MainPage = new NavigationPage(new ErrorServerPage("Serveur Introuvable"));
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
