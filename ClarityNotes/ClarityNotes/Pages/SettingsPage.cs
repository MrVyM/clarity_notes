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
            int fontSize = 0;

            if (Device.RuntimePlatform == Device.Android)
                fontSize = 20;
            else if (Device.RuntimePlatform == Device.UWP)
                fontSize = 25;
            else
                fontSize = 20;

            StackLayout mainContent = new StackLayout();
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.Center;

            Button passwordChange = new Button();
            passwordChange.Text = "Changer mon mot de passe";
            passwordChange.FontSize = fontSize;
            passwordChange.Clicked += OnPasswordChange;             
            passwordChange.BackgroundColor = Color.FromHex("249eed");
            passwordChange.CornerRadius = 25;
            passwordChange.Margin = 20;
            mainContent.Children.Add(passwordChange);

            Button nameChange = new Button();
            nameChange.Text = "Changer mon nom d'utilisateur";
            nameChange.Clicked += OnNameChange;
            nameChange.FontSize = fontSize;
            nameChange.BackgroundColor = Color.FromHex("249eed");
            nameChange.CornerRadius = 25;
            nameChange.Margin = 20;
            mainContent.Children.Add(nameChange);

            Button mailChange = new Button();
            mailChange.Text = "Changer mon mail";
            mailChange.FontSize = fontSize;
            mailChange.BackgroundColor = Color.FromHex("249eed");
            mailChange.CornerRadius = 25;
            mailChange.Margin = 20;
            mailChange.Clicked += OnMailChange;
            mainContent.Children.Add(mailChange);

            Button themeChange = new Button();
            themeChange.Text = "Changer de thème";
            themeChange.FontSize = fontSize;
            themeChange.BackgroundColor = Color.FromHex("249eed");
            themeChange.CornerRadius = 25;
            themeChange.Margin = 20;
            themeChange.Clicked += OnThemeChange;
            mainContent.Children.Add(themeChange);

            Button logOut = new Button();
            logOut.Text = "Déconnexion";
            logOut.BackgroundColor = Color.FromHex("249eed");
            logOut.CornerRadius = 25;
            logOut.Margin = 20;
            logOut.FontSize = fontSize;
            logOut.Clicked += OnLogOut;
            mainContent.Children.Add(logOut);

            Button deleteAccount = new Button();
            deleteAccount.Text = "Supprimer mon compte";
            deleteAccount.Clicked += OnDelete;
            deleteAccount.FontSize = fontSize;
            deleteAccount.BackgroundColor = Color.FromHex("249eed");
            deleteAccount.CornerRadius = 25;
            deleteAccount.Margin = 20;
            mainContent.Children.Add(deleteAccount);

            this.BackgroundColor = Color.FromHex("57b1eb");
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
        private void OnThemeChange(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ThemeChangePage(user));
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
            Navigation.PopToRootAsync();
        }
    }
}