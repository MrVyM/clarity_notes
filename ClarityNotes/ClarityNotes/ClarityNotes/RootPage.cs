using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class RootPage : ContentPage
    {
        string PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "/data";
        StackLayout listNotes;
        string PATHlistNotes;
        int fontSize;

        public RootPage()
        {
            if (App.Current.MainPage.Height < 800)
                fontSize = (int)(App.Current.MainPage.Height / 50);
            else
                fontSize = (int)(App.Current.MainPage.Height / 30);
            
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
                Directory.CreateDirectory(PATH + "/Test");
                StreamWriter sw = new StreamWriter(PATH + "/Test/Note.txt");
                sw.WriteLine("Voila ma note\n\n\n\n\nJe peux mettre autant de texte que je veux");
                sw.Close();
                
            }
           

            StackLayout mainContent = new StackLayout();
            mainContent.Orientation = StackOrientation.Horizontal;
            mainContent.VerticalOptions = LayoutOptions.CenterAndExpand;
            mainContent.HorizontalOptions = LayoutOptions.FillAndExpand;

            StackLayout verticalLayout = new StackLayout();

            Frame frameColumn = new Frame();
            frameColumn.VerticalOptions = LayoutOptions.StartAndExpand;
            frameColumn.HorizontalOptions = LayoutOptions.StartAndExpand;


            StackLayout verticalColumn = new StackLayout();
            verticalColumn.HorizontalOptions = LayoutOptions.Center;
            verticalColumn.VerticalOptions = LayoutOptions.CenterAndExpand;

            Button buttonChapter;
            foreach (string dir in Directory.EnumerateDirectories(PATH))
            {
                Console.WriteLine(dir);
                buttonChapter = new Button();
                buttonChapter.FontSize = fontSize/1.5;
                buttonChapter.BackgroundColor = Color.White;
                buttonChapter.Clicked += OnNoteClicked;
                buttonChapter.Text = Path.GetFileName(dir);
                verticalColumn.Children.Add(buttonChapter);
            }

            frameColumn.Content = verticalColumn;

            Frame AddFrame = new Frame();
            StackLayout AddLayout = new StackLayout();
            AddLayout.HorizontalOptions = LayoutOptions.End;

            Button add = new Button() { Text = "+" };
            add.Clicked += OnAddChapterClicked;
            add.FontSize = fontSize;
            add.BackgroundColor = Color.White;
            AddLayout.Children.Add(add);

            Button remove = new Button() { Text = "-" };
            remove.Clicked += OnRemoveClicked;
            remove.FontSize = fontSize;
            remove.BackgroundColor = Color.White;  
            AddLayout.Children.Add(remove);

            Button settings = new Button() { Text = "SET" };
            settings.BackgroundColor = Color.White;
            settings.FontSize = fontSize/1.5;
            AddLayout.Children.Add(settings);

            listNotes = new StackLayout();
            listNotes.Margin = 15;
            listNotes.HorizontalOptions = LayoutOptions.CenterAndExpand; 
            listNotes.VerticalOptions = LayoutOptions.CenterAndExpand;

            var direc = Directory.EnumerateDirectories(PATH);
            if (direc.Count() != 0) // si il existe un chapter
            {
                listNotes.Children.Clear();
                foreach (var dir in Directory.EnumerateFiles(direc.First()))
                {
                    Console.WriteLine(dir);
                    Button temp = new Button();
                    temp.FontSize = fontSize;
                    temp.BackgroundColor = Color.Beige;
                    temp.BorderWidth = 1;
                    temp.Text = Path.GetFileName(dir).Split('.').First().Replace('_',' ');
                    temp.Clicked += OnEditorCliked;
                    listNotes.Children.Add(temp);
                }

                PATHlistNotes = Path.GetFileName(direc.First());

                StackLayout buttonListNotes = new StackLayout();
                buttonListNotes.Orientation = StackOrientation.Horizontal;

                Button AddNote = new Button();
                AddNote.VerticalOptions = LayoutOptions.EndAndExpand;
                AddNote.Text = "Ajouter une note a " + Path.GetFileName(direc.First());
                AddNote.FontSize = fontSize;
                AddNote.BackgroundColor = Color.White;
                AddNote.Clicked += OnAddNotePageClicked;

                Button removeNote = new Button();
                removeNote.VerticalOptions = LayoutOptions.EndAndExpand;
                removeNote.Text = "Ajouter une note a " + Path.GetFileName(direc.First());
                removeNote.FontSize = fontSize;
                removeNote.BackgroundColor = Color.White;
                removeNote.Clicked += OnAddNotePageClicked;


                buttonListNotes.Children.Add(AddNote);
                buttonListNotes.Children.Add(removeNote);

                listNotes.Children.Add(buttonListNotes);
            }
            else 
            {
                Label pleaseAddChapter = new Label();
                pleaseAddChapter.Text = "Vous n'avez pas encore de chapitre. Vous pouvez en creer un en utilisant le bouton +";
                listNotes.Children.Add(pleaseAddChapter);
            }

            verticalLayout.Children.Add(verticalColumn);
            verticalLayout.Children.Add(AddLayout);
            mainContent.Children.Add(verticalLayout);
            mainContent.Children.Add(listNotes);

            this.Content = mainContent;
            this.BackgroundColor = Color.Beige;
        }
        private void OnEditorCliked(object sender, EventArgs e)
        {
            var page = new EditeurPage(PATHlistNotes + "/" + ((Button)sender).Text);
            Navigation.PushAsync(page);
        }
        private void OnNoteClicked(object sender, EventArgs e)
        {
            listNotes.Children.Clear();
            foreach (var dir in Directory.EnumerateFiles(PATH + "/" + ((Button)sender).Text))
            {
                Console.WriteLine(dir);
                Button temp = new Button();
                temp.FontSize = fontSize;
                temp.BackgroundColor = Color.Beige;
                temp.Clicked += OnEditorCliked;
                temp.Text = Path.GetFileName(dir).Split('.').First();
                listNotes.Children.Add(temp);
            }
            PATHlistNotes = Path.GetFileName(((Button)sender).Text);
            Button AddNote = new Button();
            AddNote.Text = "Ajouter une note a " + ((Button)sender).Text;
            AddNote.BackgroundColor = Color.White;
            AddNote.Clicked += OnAddNotePageClicked;
            listNotes.Children.Add(AddNote);

        }
        private void OnAddNotePageClicked(object sender, EventArgs e)
        {
            string text = ((Button)sender).Text;
            string[] splited = text.Split(' ');
            var page = new AddNotePage(splited[splited.Length -1]);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void OnAddChapterClicked(object sender, EventArgs e)
        {
            var page = new AddChapterPage();
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void OnRemoveClicked(object sender, EventArgs e)
        {
            var page = new RemoveChapterPage();
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
    }
}