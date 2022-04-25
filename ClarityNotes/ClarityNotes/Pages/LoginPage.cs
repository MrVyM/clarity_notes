using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class LoginPage : ContentPage
    {
        private Entry usernameEntry;
        private Entry passwordEntry;
        private Label errorLabel;

        public LoginPage()
        { 

            StackLayout mainContent = new StackLayout();
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.Center;
            mainContent.WidthRequest = 500;

            Frame idFrame = new Frame();
            idFrame.BackgroundColor = Color.White;
            idFrame.Margin = 25;
            idFrame.CornerRadius = 10;
            StackLayout idStack = new StackLayout();

            errorLabel = new Label();
            errorLabel.FontSize = 18;
            errorLabel.TextColor = Color.Red;
            errorLabel.HorizontalOptions = LayoutOptions.Center;
            errorLabel.VerticalOptions = LayoutOptions.Center;   
            mainContent.Children.Add(errorLabel);

            Label idLabel = new Label();
            idLabel.Text = "Nom d'utilisateur";
            idLabel.VerticalOptions = LayoutOptions.FillAndExpand;
            //idLabel.HorizontalTextAlignment = TextAlignment.Center;
            idStack.Children.Add(idLabel);

            usernameEntry = new Entry();
            idStack.Children.Add(usernameEntry);

            idFrame.Content = idStack;


            Frame mdpFrame = new Frame();
            mdpFrame.BackgroundColor = Color.White;
            mdpFrame.Margin = 25;
            mdpFrame.CornerRadius = 10;
            mdpFrame.HorizontalOptions = LayoutOptions.FillAndExpand;
            StackLayout mdpStack = new StackLayout();

            Label mdpLabel = new Label();
            mdpLabel.Text = "Mot de passe";
            //mdpLabel.HorizontalTextAlignment = TextAlignment.Center;
            mdpStack.Children.Add(mdpLabel);

            passwordEntry = new Entry();
            passwordEntry.IsPassword = true;
            mdpStack.Children.Add(passwordEntry);

            mdpFrame.Content = mdpStack;

            StackLayout buttonStack = new StackLayout();
            buttonStack.Orientation = StackOrientation.Horizontal; 
            buttonStack.HorizontalOptions = LayoutOptions.Center;
            buttonStack.VerticalOptions = LayoutOptions.FillAndExpand;

            Button connexion = new Button();
            connexion.Text = "Se connecter";
            connexion.HorizontalOptions = LayoutOptions.Center;
            connexion.VerticalOptions = LayoutOptions.Center;
            connexion.Clicked += OnConnexionClicked;
            connexion.CornerRadius = 10;
            buttonStack.Children.Add(connexion);

            Button create = new Button();
            create.Text = "S'inscrire";
            create.Clicked += OnCreatePage;
            create.HorizontalOptions = LayoutOptions.Center;
            create.VerticalOptions = LayoutOptions.Center;
            create.CornerRadius = 10;
            buttonStack.Children.Add(create);

            mainContent.Children.Add(idFrame);
            mainContent.Children.Add(mdpFrame);
            mainContent.Children.Add(buttonStack);
            this.Content = mainContent;
            this.BackgroundColor = Color.FromHex("33B0FF");
            this.Title = "ClarityNotes";
            usernameEntry.Placeholder = "Username";
            passwordEntry.Placeholder = "Password";
            //usernameEntry.HorizontalTextAlignment = TextAlignment.Center;
            //passwordEntry.HorizontalTextAlignment = TextAlignment.Center;
        }

        public void OnCreatePage(object sender, EventArgs e)
        {
            var page = new CreateAccountPage();
            Navigation.PushAsync(page);
        }

        private void OnConnexionClicked(object sender, EventArgs e)
        {
            var mdpEntryText = passwordEntry.Text;
            passwordEntry.Text = "";
            User user = User.Connexion(usernameEntry.Text, mdpEntryText);
            if (user == null)
            {
                errorLabel.Text = "Nom d'utilisateur ou mot de passe invalide";
                errorLabel.FontAttributes = FontAttributes.Bold;
            }
            else
            {
                Navigation.PopAsync();
                var page = new RootPage(user);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
            }
        }
    }
}