using System;
using System.IO;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class RemoveChapterPage : ContentPage
    {
        string PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/data";
        Picker picker;

        public RemoveChapterPage()
        {
            StackLayout mainContent = new StackLayout() { VerticalOptions = LayoutOptions.Center};

            Label questionLabel = new Label() { 
                FontSize = 16, 
                TextColor = Color.Black, 
                HorizontalTextAlignment = TextAlignment.Center };

            questionLabel.Text = "Quel chapitre souhaitez-vous supprimer ?";
            mainContent.Children.Add(questionLabel);

            picker = new Picker
            {
                Title = "Liste des chapitres",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand };

            foreach (string dir in Directory.EnumerateDirectories(PATH))
                picker.Items.Add(Path.GetFileName(dir));

            mainContent.Children.Add(picker);

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
            if (picker.SelectedItem == null) return;
            String fullPath = PATH + "/" + picker.SelectedItem.ToString();
            if (!File.Exists(fullPath)) return;
            Directory.Delete(fullPath, true);
        }
    }
}