using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class AddNotePage : ContentPage
    {
        Entry Name;
        string PATH;
        public AddNotePage(string path)
        {
            Console.WriteLine(path);
            PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/data" + "/" + path;
            StackLayout mainContent = new StackLayout();

            Label label = new Label();
            label.FontSize = 16;
            label.Text = "Veuillez entrer un nom de note.";
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
            if (Name.Text == "")
            {
                var ans = DisplayAlert("Alerte", "Une note ne peut pas avoir un nom vide.", "D'accord");
            }
            else if (!File.Exists(PATH + "/" + Name.Text + ".txt"))
            {
                StreamWriter sw = new StreamWriter(PATH + "/" + Name.Text + ".txt");
                sw.WriteLine("");
                sw.Close();

                var page = new RootPage();
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
                Navigation.RemovePage(this);
            }
            else
            {
                var ans = DisplayAlert("Alerte", "Une note existe déjà sous ce nom.", "D'accord");
                Console.WriteLine("La note existe deja");
            }

        }
    }
}