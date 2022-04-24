using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class SettingsPage : ContentPage
    {
        User user;

        public SettingsPage()
        {
            StackLayout mainContent = new StackLayout();
            Button logOut = new Button();
            logOut.Text = "Déconnexion";
            logOut.Clicked += OnLogOut;
            mainContent.Children.Add(logOut);
            this.Content = mainContent;
        }
        private void OnLogOut(Object sender, EventArgs e)
        {
            // Environment.Exit(0);
            foreach (var page in Navigation.ModalStack)
            {
                if (page != this)
                    Navigation.RemovePage(page);
            }
            Navigation.PushAsync(new LoginPage());
            Navigation.RemovePage(this);

        }
    }
}