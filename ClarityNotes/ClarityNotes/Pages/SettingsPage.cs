using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class SettingsPage : ContentPage
    {
        User user;

        public SettingsPage(User user)
        {
            this.user = user;
            StackLayout mainContent = new StackLayout();
            Button logOut = new Button();
            logOut.Text = "Déconnexion";
            logOut.Clicked += OnLogOut;
            mainContent.Children.Add(logOut);

            Button deleteAccount = new Button();
            deleteAccount.Text = "Suprimer mon compte";
            deleteAccount.Clicked += OnDelete;
            mainContent.Children.Add(deleteAccount);

            this.Content = mainContent;
        }

        private void OnDelete(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DeleteAccount(user));
        }

        private void OnLogOut(Object sender, EventArgs e)
        {
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