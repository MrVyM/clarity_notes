using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class AddChapterPage : ContentPage
    {
        private User user;
        private Entry nameEntry;

        public AddChapterPage(User user)
        {
            this.user = user;

            StackLayout mainContent = new StackLayout();

            Label label = new Label();
            label.FontSize = 16;
            label.Text = "Veuillez entrer un nom de chapitre.";
            mainContent.Children.Add(label);
            
            nameEntry = new Entry();
            mainContent.Children.Add(nameEntry);

            Button submit = new Button();
            submit.Clicked += OnSubmitClicked;
            submit.Text = "Valider";
            mainContent.Children.Add(submit);

            this.Content = mainContent;
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