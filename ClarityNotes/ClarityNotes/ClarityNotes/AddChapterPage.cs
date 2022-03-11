using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class AddChapterPage : ContentPage
    {
        Entry Name;
        public AddChapterPage()
        {
            StackLayout mainContent = new StackLayout();

            Label label = new Label();
            label.FontSize = 16;
            label.Text = "Veuillez entrer un nom de chapitre.";
            mainContent.Children.Add(label);
            
            Name = new Entry();
            mainContent.Children.Add(Name);

            Button submit = new Button();
            submit.Clicked += OnSubmitClicked;
            submit.Text = "Valider";
            mainContent.Children.Add(submit);

            this.Content = mainContent;
        }
        private void OnSubmitClicked(object sender, EventArgs e)
        {
            string PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/data";
            if (!Directory.Exists(PATH +"/"+ Name.Text))
            {
                Directory.CreateDirectory(PATH +"/"+ Name.Text);
                
                var page = new RootPage();
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
                Navigation.RemovePage(this);
            }
            else 
            {
                var ans = DisplayAlert("Alerte", "Un chapitre existe déjà sous ce nom.", "D'accord");
                Console.WriteLine("Le directory existe deja");
            }
            
        }
    }
}