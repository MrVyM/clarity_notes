using System;
using System.Text.RegularExpressions;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class NewMailPage : ContentPage
    {
        User user;
        Entry mailEntry;
        public NewMailPage(User user)
        {
            this.user = user;

            StackLayout mainContent = new StackLayout();
            mainContent.Padding = 50;
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.CenterAndExpand;

            Frame frame = new Frame();
            frame.HasShadow = true;
            frame.HorizontalOptions = LayoutOptions.Center;
            frame.BackgroundColor = Color.White;

            StackLayout framStack = new StackLayout();

            Label label = new Label();
            label.FontSize = 16;
            label.Text = $"Veuillez entrer votre nouveau mail\n\nVotre ancien email : {user.Email}";
            framStack.Children.Add(label);

            mailEntry = new Entry();
            mailEntry.Completed += OnSubmitClicked;
            framStack.Children.Add(mailEntry);

            Button submit = new Button();
            submit.HorizontalOptions = LayoutOptions.Center;
            submit.VerticalOptions = LayoutOptions.Center;
            submit.Clicked += OnSubmitClicked;
            submit.Margin = 20;
            submit.Text = "Valider";

            frame.Content = framStack;
            mainContent.Children.Add(frame);
            mainContent.Children.Add(submit);

            this.Content = mainContent;
            this.BackgroundColor = user.ColorTheme;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (mailEntry.Text == null) return;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(mailEntry.Text.ToLower());
            if (!match.Success)
            {
                DisplayAlert("Changement échoué", "L'adresse mail n'est pas correcte (format invalide).", "OK");
                mailEntry.Text = "";
                return;
            }
            User.Change(user.Id, "email", mailEntry.Text);
            var page = new RootPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
        }
    }
}