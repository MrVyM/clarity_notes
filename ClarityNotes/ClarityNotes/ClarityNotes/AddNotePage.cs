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
            PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "/data" + "/" + path;
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
            label.Text = "Veuillez entrer un nom de note.";
            framStack.Children.Add(label);

            Name = new Entry();
            framStack.Children.Add(Name);

            Button submit = new Button();
            submit.HorizontalOptions = LayoutOptions.Center;
            submit.VerticalOptions = LayoutOptions.Center;
            submit.Clicked += OnSubmitClicked;
            submit.Text = "Valider";

            frame.Content = framStack;
            mainContent.Children.Add(frame);


            mainContent.Children.Add(submit);  
            this.Content = mainContent;
        }

        private static bool CheckName(string input)
        {
            List<char> symbol = new List<char>{'<', '>', ':', '“', '/', '\'', '|', '?' };
            foreach (char c in symbol)
            {
                if (input.Contains(c))
                    return true;
            }
            return false;
        }


        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (Name.Text == "" || CheckName(Name.Text))
            {
                var ans = DisplayAlert("Alerte", "Ce nom n'est pas valide. Vuillez en choisir un autre.", "D'accord");
            }
            else if (!File.Exists(PATH + "/" + Name.Text + ".txt"))
            {
                if(Name.Text.Contains(' '))
                    Name.Text = Name.Text.Replace(' ', '_');
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