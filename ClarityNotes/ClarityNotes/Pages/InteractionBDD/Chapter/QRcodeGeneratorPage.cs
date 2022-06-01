using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class QRcodeGeneratorPage : ContentPage
    {
        private User user;
        private Entry nameEntry;

        public QRcodeGeneratorPage(User user)
        {
            this.user = user;

            StackLayout mainContent = new StackLayout();
            mainContent.Padding = 50;
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.CenterAndExpand;

            Frame frame = new Frame();
            frame.HasShadow = true;
            frame.BackgroundColor = Color.Beige;
            frame.HorizontalOptions = LayoutOptions.Center;
            StackLayout framStack = new StackLayout();

            Label label = new Label();
            label.FontSize = 16;
            label.Text = "Veuillez entrer un nom de chapitre.";
            framStack.Children.Add(label);

            nameEntry = new Entry();
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
            this.BackgroundColor = Color.FromHex("57b1eb");
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (Directory.CreateDirectory(nameEntry.Text, user))
            {   
                var page = new RootPage(user);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
                Navigation.RemovePage(this);
            }
            else
            {
                var ans = DisplayAlert("Création impossible", "Un chapitre existe déjà sous ce nom.", "OK");
            }
        }
    }
}