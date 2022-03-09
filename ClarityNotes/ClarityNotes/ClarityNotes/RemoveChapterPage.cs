using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class RemoveChapterPage : ContentPage
    {
        Entry Name;
        public RemoveChapterPage()
        {
            StackLayout mainContent = new StackLayout() { VerticalOptions = LayoutOptions.Center};

            Label questionLabel = new Label() { FontSize = 16, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Center };
            questionLabel.Text = "Quel chapitre souhaitez-vous supprimer ?";
            mainContent.Children.Add(questionLabel);

            Name = new Entry();
            mainContent.Children.Add(Name);

            Button submit = new Button() { Text = "Confirmation" };
            submit.Clicked += OnSubmitClicked;
            mainContent.Children.Add(submit);

            Label warningLabel = new Label() { FontSize = 16, TextColor = Color.Red, HorizontalTextAlignment = TextAlignment.Center };
            warningLabel.Text = "Attention, la suppression d'un chapitre est irréversible et " +
                "supprime définitivement toutes les notes incluses dans ce dernier.";
            mainContent.Children.Add(warningLabel);

            this.Content = mainContent;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            // TODO : TO IMPLEMENT SOON   
        }
    }
}