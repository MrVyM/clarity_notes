using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class RemoveChapterPage : ContentPage
    {
        private User user;
        private Picker picker;

        public RemoveChapterPage(User user)
        {
            this.user = user;

            StackLayout mainContent = new StackLayout() {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 500 };

            StackLayout stackLayout = new StackLayout();

            Label questionLabel = new Label() { 
                FontSize = 16,
                Text = "Quel chapitre souhaitez-vous supprimer ?",
                TextColor = Color.Black, 
                HorizontalTextAlignment = TextAlignment.Center };

            picker = new Picker {
                Title = "Sélection du chapitre",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand };

            foreach (Directory directory in Directory.GetUserDirectories(user))
                picker.Items.Add(directory.Title);

            Button submit = new Button() { 
                Text = "Confirmation",
                HorizontalOptions = LayoutOptions.Center };
            submit.Clicked += OnSubmitClicked;
            
            Label warningLabel = new Label() { 
                FontSize = 12,
                Text = "Attention, la suppression d'un chapitre est irréversible et " +
                "supprime définitivement toutes les notes incluses dans ce dernier.",
                TextColor = Color.Red, 
                HorizontalTextAlignment = TextAlignment.Center };

            stackLayout.Children.Add(questionLabel);
            stackLayout.Children.Add(picker);
            stackLayout.Children.Add(warningLabel);

            mainContent.Children.Add(new Frame() { Margin = 25, Content = stackLayout });
            mainContent.Children.Add(submit);
            this.Content = mainContent;
            this.BackgroundColor = Color.FromHex("57b1eb");

        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (picker.SelectedItem == null) return;
            if (Directory.DeleteDirectory(Directory.GetDirectoryByTitleAndIdOwner(picker.SelectedItem.ToString(), user).Id, user))
            {
                var page = new RootPage(user);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
                Navigation.RemovePage(this);
            }
        }
    }
}