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

            foreach (string dir in Directory.EnumerateDirectories(PATH))
                picker.Items.Add(Path.GetFileName(dir));

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
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (picker.SelectedItem == null) return;
            string fullPath = PATH + "/" + picker.SelectedItem.ToString();
            if (!Directory.Exists(fullPath)) return;
            Directory.Delete(fullPath, true);
            var page = new RootPage();
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
            Navigation.RemovePage(this);
        }
    }
}