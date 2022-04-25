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

            Button passwordChange = new Button();
            passwordChange.Text = "Changer mon mot de passe";
            passwordChange.Clicked += OnPasswordChange;
            mainContent.Children.Add(passwordChange);

            Button nameChange = new Button();
            nameChange.Text = "Changer mon nom d'utilisateur";
            nameChange.Clicked += OnNameChange;
            mainContent.Children.Add(nameChange);

            Button mailChange = new Button();
            mailChange.Text = "Changer mon mail";
            mailChange.Clicked += OnMailChange;
            mainContent.Children.Add(mailChange);
            
            this.Content = mainContent;
        }

        private void OnDelete(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DeleteAccount(user));
        }
        private void OnMailChange(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GetPasswordPage(user, "mail", "Changement de l'adresse mail"));
        }

        private void OnNameChange(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GetPasswordPage(user, "name", "Changement du nom d'utilisateur"));
        }
        private void OnPasswordChange(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GetPasswordPage(user, "password", "Changement du mot de passe"));
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