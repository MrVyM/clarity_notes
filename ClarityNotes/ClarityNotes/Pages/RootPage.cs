using System;
using System.Linq;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class RootPage : ContentPage
    {
        private User user;
        private StackLayout stackListNotes;
        private StackLayout stackButtonListNotes;
        private int fontSize;
        private Directory selectedDirectory;

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

            Frame frameColumn = new Frame();
            frameColumn.VerticalOptions = LayoutOptions.StartAndExpand;
            frameColumn.HorizontalOptions = LayoutOptions.StartAndExpand;


            StackLayout verticalColumn = new StackLayout();
            verticalColumn.HorizontalOptions = LayoutOptions.Center;
            verticalColumn.VerticalOptions = LayoutOptions.CenterAndExpand;

            Button buttonChapter;
            foreach (Directory directory in directories)
            {
                buttonChapter = new Button();
                buttonChapter.FontSize = fontSize/1.5;
                buttonChapter.BackgroundColor = Color.White;
                buttonChapter.Clicked += OnDirectoryButtonClicked;
                buttonChapter.Text = directory.Title;
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
            remove.Clicked += OnRemoveChapterClicked;
            remove.FontSize = fontSize;
            remove.BackgroundColor = Color.White;  
            AddLayout.Children.Add(remove);

            Button settings = new Button() { Text = "SET" };
            settings.BackgroundColor = Color.White;
            settings.FontSize = fontSize/1.5;
            AddLayout.Children.Add(settings);

            stackListNotes = new StackLayout();
            stackListNotes.Margin = 15;
            stackListNotes.HorizontalOptions = LayoutOptions.CenterAndExpand; 
            stackListNotes.VerticalOptions = LayoutOptions.CenterAndExpand;
            if (directories.Any())
            {
                stackListNotes.Children.Clear();
                selectedDirectory = directories[0];
                foreach (Note note in Note.GetNotesByIdDirectory(directories[0].Id))
                {
                    Button temp = new Button();
                    temp.FontSize = fontSize;
                    temp.BackgroundColor = Color.Beige;
                    temp.BorderWidth = 1;
                    temp.Text = note.Title;
                    temp.Clicked += OnEditorClicked;
                    stackListNotes.Children.Add(temp);
                }

                this.stackButtonListNotes = new StackLayout();
                stackButtonListNotes.Orientation = StackOrientation.Horizontal;

                Button AddNote = new Button();
                AddNote.VerticalOptions = LayoutOptions.EndAndExpand;
                AddNote.Text = "Ajouter une note à " + directories[0].Title;
                AddNote.FontSize = fontSize;
                AddNote.BackgroundColor = Color.White;
                AddNote.Clicked += OnAddNoteClicked;

                Button removeNote = new Button();
                removeNote.VerticalOptions = LayoutOptions.EndAndExpand;
                removeNote.Text = "Retirer une note à " + directories[0].Title;
                removeNote.FontSize = fontSize;
                removeNote.BackgroundColor = Color.White;
                removeNote.Clicked += OnAddNoteClicked;


                stackButtonListNotes.Children.Add(AddNote);
                stackButtonListNotes.Children.Add(removeNote);

                stackListNotes.Children.Add(stackButtonListNotes);
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
            this.BackgroundColor = Color.Beige;
        }
        private void OnEditorClicked(object sender, EventArgs e)
        {
            var page = new EditorPage(Note.GetNoteByTitle(((Button)sender).Text), user);
            Navigation.PushAsync(page);
        }
        private void OnDirectoryButtonClicked(object sender, EventArgs e)
        {
            stackListNotes.Children.Clear();
            selectedDirectory = Directory.GetDirectoryByTitle(((Button)sender).Text);
            foreach (Note note in Note.GetNotesByIdDirectory(selectedDirectory.Id))
            {
                Button temp = new Button();
                temp.FontSize = fontSize;
                temp.BackgroundColor = Color.Beige;
                temp.Clicked += OnEditorClicked;
                temp.Text = note.Title;
                stackListNotes.Children.Add(temp);
            }

            Button addNote = new Button();
            addNote.VerticalOptions = LayoutOptions.EndAndExpand;
            addNote.Text = "Ajouter une note à " + selectedDirectory.Title;
            addNote.FontSize = fontSize;
            addNote.BackgroundColor = Color.White;
            addNote.Clicked += OnAddNoteClicked;

            Button removeNote = new Button();
            removeNote.VerticalOptions = LayoutOptions.EndAndExpand;
            removeNote.Text = "Retirer une note à " + selectedDirectory.Title;
            removeNote.FontSize = fontSize;
            removeNote.BackgroundColor = Color.White;
            removeNote.Clicked += OnAddNoteClicked;

            stackButtonListNotes.Children.Clear();
            stackButtonListNotes.Children.Add(addNote);
            stackButtonListNotes.Children.Add(removeNote);
            stackListNotes.Children.Remove(stackButtonListNotes);
            stackListNotes.Children.Add(stackButtonListNotes);

        }
        private void OnAddNoteClicked(object sender, EventArgs e)
        {
            var page = new AddNotePage(user, selectedDirectory);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void OnAddChapterClicked(object sender, EventArgs e)
        {
            var page = new AddChapterPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void OnRemoveChapterClicked(object sender, EventArgs e)
        {
            var page = new RemoveChapterPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
    }
}