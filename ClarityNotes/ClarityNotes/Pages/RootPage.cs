using System;
using System.Linq;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class RootPage : ContentPage
    {
        private User user;
        private StackLayout stackListNotes;
        private string PATHlistNotes;
        private int fontSize;

        public RootPage(User user)
        {
            this.user = user;

            if (App.Current.MainPage.Height < 800)
                fontSize = (int)(App.Current.MainPage.Height / 50);
            else
                fontSize = (int)(App.Current.MainPage.Height / 30);

            Directory[] directories = Directory.GetAllDirectories();

            StackLayout mainContent = new StackLayout();
            mainContent.Orientation = StackOrientation.Horizontal;
            mainContent.VerticalOptions = LayoutOptions.CenterAndExpand;
            mainContent.HorizontalOptions = LayoutOptions.FillAndExpand;

            StackLayout verticalLayout = new StackLayout();


            StackLayout verticalColumn = new StackLayout();
            verticalColumn.HorizontalOptions = LayoutOptions.Start;
            verticalColumn.VerticalOptions = LayoutOptions.CenterAndExpand;

            Button buttonChapter;
            foreach (Directory directory in directories)
            {
                buttonChapter = new Button();
                buttonChapter.FontSize = fontSize/1.5;
                buttonChapter.BackgroundColor = Color.White;
                buttonChapter.Clicked += OnNoteClicked;
                buttonChapter.Text = directory.Title;
                verticalColumn.Children.Add(buttonChapter);
            }
            StackLayout AddLayout = new StackLayout();
            AddLayout.HorizontalOptions = LayoutOptions.Center;
            AddLayout.VerticalOptions = LayoutOptions.End;

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
            settings.Clicked += OnSettingsPageCliked;
            settings.FontSize = fontSize/1.5;
            AddLayout.Children.Add(settings);

            stackListNotes = new StackLayout();
            stackListNotes.Margin = 15;
            stackListNotes.HorizontalOptions = LayoutOptions.CenterAndExpand; 
            stackListNotes.VerticalOptions = LayoutOptions.CenterAndExpand;

            if (directories.Any())
            {
                stackListNotes.Children.Clear();
                foreach (Note note in Note.GetNotesByIdDirectory(directories[0].Id))
                {
                    Button temp = new Button();
                    temp.FontSize = fontSize;
                    temp.BackgroundColor = Color.FromHex("57b1eb");
                    temp.BorderWidth = 1;
                    temp.Text = note.Title;
                    temp.Clicked += OnEditorClicked;
                    stackListNotes.Children.Add(temp);
                }

                StackLayout buttonListNotes = new StackLayout();
                buttonListNotes.Orientation = StackOrientation.Horizontal;

                Button AddNote = new Button();
                AddNote.VerticalOptions = LayoutOptions.EndAndExpand;
                AddNote.Text = "Ajouter une note à " + directories[0].Title;
                AddNote.FontSize = fontSize;
                AddNote.BackgroundColor = Color.White;
                AddNote.Clicked += OnAddNotePageClicked;

                Button removeNote = new Button();
                removeNote.VerticalOptions = LayoutOptions.EndAndExpand;
                removeNote.Text = "Retirer une note à " + directories[0].Title;
                removeNote.FontSize = fontSize;
                removeNote.BackgroundColor = Color.White;
                removeNote.Clicked += OnRemoveNotePageCliked;


                buttonListNotes.Children.Add(AddNote);
                buttonListNotes.Children.Add(removeNote);

                stackListNotes.Children.Add(buttonListNotes);
            }
            else 
            {
                Label pleaseAddChapter = new Label();
                pleaseAddChapter.Text = "Vous n'avez pas encore de chapitre. Vous pouvez en créer un en utilisant le bouton +";
                stackListNotes.Children.Add(pleaseAddChapter);
            }

            verticalLayout.Children.Add(verticalColumn);
            verticalLayout.Children.Add(AddLayout);
            mainContent.Children.Add(verticalLayout);
            mainContent.Children.Add(stackListNotes);

            this.Content = mainContent;
            this.BackgroundColor = Color.FromHex("57b1eb");
        }
        private void OnEditorClicked(object sender, EventArgs e)
        {
            var page = new EditorPage(Note.GetNoteByTitle(((Button)sender).Text), user);
            Navigation.PushAsync(page);
        }
        private void OnNoteClicked(object sender, EventArgs e)
        {
            stackListNotes.Children.Clear();
            foreach (Note note in Note.GetNotesByIdDirectory(Directory.GetDirectoryByTitle(((Button)sender).Text).Id))
            {
                Button temp = new Button();
                temp.FontSize = fontSize;
                temp.BackgroundColor = Color.FromHex("57b1eb");
                temp.Clicked += OnEditorClicked;
                temp.Text = note.Title;
                stackListNotes.Children.Add(temp);
            }
            //PATHlistNotes = Path.GetFileName(((Button)sender).Text);
            Button AddNote = new Button();
            AddNote.Text = "Ajouter une note à " + ((Button)sender).Text;
            AddNote.BackgroundColor = Color.White;
            AddNote.Clicked += OnAddNotePageClicked;
            stackListNotes.Children.Add(AddNote);

        }
        private void OnAddNotePageClicked(object sender, EventArgs e)
        {
            string text = ((Button)sender).Text;
            string[] splited = text.Split(' ');
            var page = new AddNotePage(user);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void OnSettingsPageCliked(object sender, EventArgs e)
        {
            var page = new SettingsPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
        private void OnAddChapterClicked(object sender, EventArgs e)
        {
            var page = new AddChapterPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void OnRemoveNotePageCliked(object sender, EventArgs e)
        {
            var page = new RemoveNotePage(user);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void OnRemoveClicked(object sender, EventArgs e)
        {
            var page = new RemoveChapterPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
    }
}