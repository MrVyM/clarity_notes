using System;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class NewNamePage : ContentPage
    {
        User user;
        Entry nameEntry;
        public NewNamePage(User user)
        {
            this.user = user;

            StackLayout mainContent = new StackLayout();
            mainContent.Padding = 50;
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.CenterAndExpand;

            Frame frame = new Frame();
            frame.HasShadow = true;
            frame.BackgroundColor = Color.White;
            frame.HorizontalOptions = LayoutOptions.Center;

            StackLayout framStack = new StackLayout();

            Label label = new Label();
            label.FontSize = 16;
            label.Text = $"Veuillez entrer votre nouveau nom d'utilisateur\n\nVotre ancien nom : {user.Username}";
            framStack.Children.Add(label);

            nameEntry = new Entry();
            nameEntry.Completed += OnSubmitClicked;
            framStack.Children.Add(nameEntry);

            Button submit = new Button();
            submit.HorizontalOptions = LayoutOptions.Center;
            submit.VerticalOptions = LayoutOptions.Center;
            submit.Clicked += OnSubmitClicked;
            submit.Text = "Valider";

            frame.Content = framStack;
            mainContent.Children.Add(frame);
            mainContent.Children.Add(submit);

            this.Content = mainContent;
            this.BackgroundColor = user.ColorTheme;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (nameEntry.Text == null) return;
            User.Change(user.Id, "username", nameEntry.Text);
            user.Username = nameEntry.Text;
            var page = new RootPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
        }
    }
}