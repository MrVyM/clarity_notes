using System;
using System.Linq;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class RootPage : ContentPage
    {
        private User user;
        private StackLayout stackNotes;
        private double divisor;
        private int currentDirectory;
        private int fontsize = 30;

        public double FontSize { get; }

        public RootPage(User user)
        {
            this.user = user;

            if (Device.RuntimePlatform == Device.UWP)
                divisor = 0.8;
            else
            {
                fontsize = 13;
                divisor = 2;
            }   

            Directory[] directories = Directory.GetAllDirectories();

            StackLayout mainContent = new StackLayout();
            mainContent.Orientation = StackOrientation.Horizontal;
            mainContent.HorizontalOptions = LayoutOptions.FillAndExpand;

            StackLayout verticalLayout = new StackLayout();
            verticalLayout.VerticalOptions = LayoutOptions.FillAndExpand;
            verticalLayout.Margin = 20/divisor;

            StackLayout verticalColumn = new StackLayout();
            verticalColumn.VerticalOptions = LayoutOptions.StartAndExpand;

            Button buttonChapter;
            foreach (Directory directory in directories)
            {
                buttonChapter = new Button();
                buttonChapter.FontSize = fontsize / 1.2;
                buttonChapter.BackgroundColor = Color.White;
                buttonChapter.Clicked += OnNoteClicked;
                buttonChapter.CornerRadius = 5;
                buttonChapter.Text = directory.Title;
                verticalColumn.Children.Add(buttonChapter);
            }
            StackLayout AddLayout = new StackLayout();
            AddLayout.HorizontalOptions = LayoutOptions.Center;
            AddLayout.VerticalOptions = LayoutOptions.End;

            Button add = new Button() { Text = "+" };
            add.Clicked += OnAddChapterClicked;
            add.FontSize = fontsize;
            add.CornerRadius = 10;
            add.Margin = 10/divisor;
            add.BackgroundColor = Color.White;
            AddLayout.Children.Add(add);

            Button remove = new Button() { Text = "-" };
            remove.Clicked += OnRemoveClicked;
            remove.FontSize = fontsize;
            remove.CornerRadius = 10;
            remove.Margin = 10/divisor;
            remove.BackgroundColor = Color.White;
            AddLayout.Children.Add(remove);

            Button settings = new Button() { Text = "SET" };
            settings.BackgroundColor = Color.White;
            settings.CornerRadius = 10;
            settings.Margin = 10/divisor;
            settings.Clicked += OnSettingsPageCliked;
            settings.FontSize = fontsize;
            AddLayout.Children.Add(settings);

            Button Website = new Button() { Text = "Site Internet" };
            Website.BackgroundColor = Color.White;
            Website.CornerRadius = 10;
            Website.Margin = 10;
            Website.Clicked += OnWebSiteCliked;
            Website.FontSize = fontsize;



            stackNotes = new StackLayout();
            stackNotes.Margin = 15/divisor;
            stackNotes.HorizontalOptions = LayoutOptions.CenterAndExpand;

            if (directories != new Directory[] { })
            {
                StackLayout stackListNotes = new StackLayout();
                stackListNotes.Margin = 15/divisor;
                stackListNotes.VerticalOptions = LayoutOptions.CenterAndExpand;
                stackListNotes.HorizontalOptions = LayoutOptions.FillAndExpand;
                stackListNotes.Children.Clear();
                currentDirectory = directories[0].Id;
                Note[] notes = Note.GetNotesByIdDirectory(directories[0].Id);
                foreach (Note note in notes)
                {
                    Button temp = new Button();
                    temp.FontSize = fontsize;
                    temp.BackgroundColor = Color.FromHex("249eed");
                    temp.CornerRadius = 25;
                    temp.BorderWidth = 1;
                    temp.Text = note.Title;
                    temp.FontAttributes = FontAttributes.Italic;
                    temp.Clicked += OnEditorClicked;
                    stackListNotes.Children.Add(temp);
                }

                stackNotes.Children.Add(stackListNotes);

                StackLayout buttonListNotes = new StackLayout();
                buttonListNotes.Orientation = StackOrientation.Horizontal;
                buttonListNotes.VerticalOptions = LayoutOptions.End;
                buttonListNotes.Margin = 20/divisor;

                Button AddNote = new Button();
                AddNote.VerticalOptions = LayoutOptions.EndAndExpand;
                if (Device.RuntimePlatform == Device.UWP)
                    AddNote.Text = "Ajouter une note";
                else
                    AddNote.Text = "Ajouter";
                AddNote.FontSize = fontsize;
                AddNote.BackgroundColor = Color.White;
                AddNote.Clicked += OnAddNotePageClicked;
                buttonListNotes.Children.Add(AddNote);

                Button removeNote = new Button();
                removeNote.VerticalOptions = LayoutOptions.EndAndExpand;
                removeNote.Text = "Retirer";
                removeNote.FontSize = fontsize;
                removeNote.BackgroundColor = Color.White;
                removeNote.Clicked += OnRemoveNotePageCliked;

                if (Device.RuntimePlatform == Device.UWP)
                {
                    AddNote.Text = "Ajouter une note";
                    removeNote.Text = "Retirer une note";
                }
               

                if (notes.Length == 0) {
                    removeNote.IsEnabled = false;
                }

                buttonListNotes.Children.Add(removeNote);

                stackNotes.Children.Add(buttonListNotes);
            }
            else
            {
                Label pleaseAddChapter = new Label();
                pleaseAddChapter.Text = "Vous n'avez pas encore de chapitre. Vous pouvez en créer un en utilisant le bouton +";
                stackNotes.Children.Add(pleaseAddChapter);
            }

            verticalLayout.Children.Add(verticalColumn);
            verticalLayout.Children.Add(AddLayout);
            mainContent.Children.Add(verticalLayout);
            mainContent.Children.Add(stackNotes);

            this.Content = mainContent;
            this.BackgroundColor = Color.FromHex("57b1eb");
        }

        private void test(object sender, EventArgs e)
        {
            var page = new SettingsPage(user);
            Navigation.PushAsync(page);
        }

        private void OnEditorClicked(object sender, EventArgs e)
        {
            var page = new EditorPage(Note.GetNoteByTitle(((Button)sender).Text), user);
            Navigation.PushAsync(page);
        }
        private void OnNoteClicked(object sender, EventArgs e)
        {
            stackNotes.Children.Clear();
            currentDirectory = Directory.GetDirectoryByTitle(((Button)sender).Text).Id;
            Note[] notes = Note.GetNotesByIdDirectory(currentDirectory);
            StackLayout stackListNotes = new StackLayout();
            stackListNotes.Margin = 15 / divisor;
            stackListNotes.VerticalOptions = LayoutOptions.CenterAndExpand;
            stackListNotes.HorizontalOptions = LayoutOptions.FillAndExpand;
            stackListNotes.Children.Clear();
            foreach (Note note in notes)
            {
                Button temp = new Button();
                temp.FontSize = fontsize;
                temp.BackgroundColor = Color.FromHex("249eed");
                temp.BorderWidth = 1;
                temp.Text = note.Title;
                temp.CornerRadius = 25;
                temp.FontAttributes = FontAttributes.Italic;
                temp.Clicked += OnEditorClicked;
                stackListNotes.Children.Add(temp);
            }

            stackNotes.Children.Add(stackListNotes);

            StackLayout buttonListNotes = new StackLayout();
            buttonListNotes.Orientation = StackOrientation.Horizontal;
            buttonListNotes.VerticalOptions = LayoutOptions.End;
            buttonListNotes.Margin = 20/ divisor;

            Button AddNote = new Button();
            AddNote.VerticalOptions = LayoutOptions.EndAndExpand;
            AddNote.Text = "Ajouter";
            AddNote.FontSize = fontsize;
            AddNote.BackgroundColor = Color.White;
            AddNote.Clicked += OnAddNotePageClicked;
            buttonListNotes.Children.Add(AddNote);

            Button removeNote = new Button();
            removeNote.VerticalOptions = LayoutOptions.EndAndExpand;
            removeNote.Text = "Retirer";
            removeNote.FontSize = fontsize;
            removeNote.BackgroundColor = Color.White;
            removeNote.Clicked += OnRemoveNotePageCliked;

            if (Device.RuntimePlatform == Device.UWP)
            {
                AddNote.Text = "Ajouter une note";
                removeNote.Text = "Retirer une note";
            }

            if (notes.Length == 0)
            {
                removeNote.IsEnabled = false;
            }
             
            buttonListNotes.Children.Add(removeNote);
            stackNotes.Children.Add(buttonListNotes);
        }
        private void OnAddNotePageClicked(object sender, EventArgs e)
        {
            string text = ((Button)sender).Text;
            string[] splited = text.Split(' ');
            var page = new AddNotePage(user, Directory.GetDirectoryById(currentDirectory));
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void OnSettingsPageCliked(object sender, EventArgs e)
        {
            var page = new SettingsPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void OnWebSiteCliked(object sender, EventArgs e)
        {
            var page = new WebPage("claritynotes.ml");
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
            var page = new RemoveNotePage(user,currentDirectory);
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