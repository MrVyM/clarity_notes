using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class CreateAccountPage : ContentPage
    {
        private Entry usernameEntry;
        private Entry emailEntry;
        private Entry passwordEntry;
        private Entry passwordConfirmEntry;
        private Button confirmButton;

        public CreateAccountPage()
        {
            StackLayout content = new StackLayout();
            content.VerticalOptions = LayoutOptions.CenterAndExpand;
            content.Padding = 50;

            Frame email = new Frame();
            email.BackgroundColor = Color.White;
            email.Margin = 20;
            email.CornerRadius = 10;

            StackLayout emailStack = new StackLayout();

            Label emailLabel = new Label();
            emailLabel.Text = "Insérez votre adresse mail :";
            emailStack.Children.Add(emailLabel);

            emailEntry = new Entry();
            emailStack.Children.Add(emailEntry);


            email.Content = emailStack;
            content.Children.Add(email);

            Frame username = new Frame();
            username.BackgroundColor = Color.White;
            username.Margin = 20;
            username.CornerRadius = 10;

            StackLayout userStack = new StackLayout();

            Label userLabel = new Label();
            userLabel.Text = "Choisissez un nom d'utilisateur :";
            userStack.Children.Add(userLabel);

            usernameEntry = new Entry();
            userStack.Children.Add(usernameEntry);

            username.Content = userStack;

            content.Children.Add(username);

            Frame password = new Frame();
            password.Margin = 20;
            password.BackgroundColor = Color.White;
            password.CornerRadius = 10;

            StackLayout passStack = new StackLayout();

            Label passWordLabel = new Label();
            passWordLabel.Text = "Choisissez votre mot de passe :\n\n" +
            "Minimum 8 caractères, au moins une majuscule, une minuscule, un nombre et un caractère spécial.";
            passStack.Children.Add(passWordLabel);

            passwordEntry = new Entry();
            passwordEntry.IsPassword = true;
            passStack.Children.Add(passwordEntry);

            Label passNewLabel = new Label();
            passNewLabel.Text = "Confirmez votre mot de passe :";
            passStack.Children.Add(passNewLabel);

            passwordConfirmEntry = new Entry();
            passwordConfirmEntry.IsPassword = true;
            passwordConfirmEntry.TextChanged += OnCompare;
            passStack.Children.Add(passwordConfirmEntry);

            password.Content = passStack;
            content.Children.Add(password);


            confirmButton = new Button();
            confirmButton.IsEnabled = false;
            confirmButton.BackgroundColor = Color.FromHex("298dcc");
            confirmButton.Text = "S'inscrire";
            confirmButton.CornerRadius = 10;
            confirmButton.HorizontalOptions = LayoutOptions.Center;
            confirmButton.VerticalOptions = LayoutOptions.Center;

            confirmButton.Clicked += OnCreateClicked;

            content.Children.Add(confirmButton);
            Content = content;

            usernameEntry.Placeholder = "Username";
            emailEntry.Placeholder = "Email";
            passwordEntry.Placeholder = "Password";
            passwordConfirmEntry.Placeholder = "Password";
            this.BackgroundColor = Database.DEFAULT_COLOR;
        }

        public void OnCompare(object sender, EventArgs e)
        {
            // Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
            Regex passwordType = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            Match match = passwordType.Match(passwordConfirmEntry.Text);
            if (!(match.Success && passwordConfirmEntry.Text == passwordEntry.Text))
            {
                confirmButton.IsEnabled = false;
                passwordEntry.TextColor = Color.Red;
                passwordConfirmEntry.TextColor = Color.Red;
            }
            else
            {
                confirmButton.IsEnabled = true;
                passwordEntry.TextColor = Color.Blue;
                passwordConfirmEntry.TextColor = Color.Blue;
            }
        }

        private void OnCreateClicked(object sender, EventArgs e)
        {
            if (usernameEntry.Text.Contains("@") || usernameEntry.Text == null)
            {
                DisplayAlert("Inscription échouée", "Le nom d'utilisateur n'est pas correcte (il ne doit pas contenir de @).", "OK");
                usernameEntry.Text = "";
                return;
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailEntry.Text.ToLower());
            if (!match.Success)
            {
                DisplayAlert("Inscription échouée", "L'adresse mail n'est pas correcte (format invalide).", "OK");
                emailEntry.Text = "";
                return;
            }
            bool result = User.CreateUser(emailEntry.Text.ToLower(), usernameEntry.Text, passwordEntry.Text);
            if (result)
            {
                DisplayAlert("Inscription effectuée", "Votre compte a été crée avec succès.", "OK");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                usernameEntry.Text = "";
                emailEntry.Text = "";
                passwordEntry.Text = "";
                passwordConfirmEntry.Text = "";
                DisplayAlert("Inscription échouée", "Un compte possède déjà ce nom d'utilisateur ou cette email.", "OK");
            }
        }
    }
}