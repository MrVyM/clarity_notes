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
            Application.Current.MainPage = new NavigationPage(this);

            if (Device.RuntimePlatform == Device.UWP)
                divisor = 0.8;
            else
            {
                fontsize = 13;
                divisor = 2;
            }   

            Directory[] directories = Directory.GetUserDirectories(user);
            SortList(directories);

            StackLayout mainContent = new StackLayout();
            mainContent.Orientation = StackOrientation.Horizontal;
            mainContent.HorizontalOptions = LayoutOptions.FillAndExpand;

            StackLayout verticalLayout = new StackLayout();
            verticalLayout.VerticalOptions = LayoutOptions.FillAndExpand;
            verticalLayout.Margin = 20/divisor;

            StackLayout verticalColumn = new StackLayout();
            verticalColumn.VerticalOptions = LayoutOptions.StartAndExpand;
            verticalColumn.Margin = 10/divisor;

            Button buttonChapter;
            foreach (Directory directory in directories)
            {
                buttonChapter = new Button();
                buttonChapter.FontSize = fontsize / 1.2;
                buttonChapter.BackgroundColor = Color.White;
                buttonChapter.Clicked += OnNoteClicked;
                buttonChapter.CornerRadius = 5;
                buttonChapter.Padding = 5 / divisor;
                buttonChapter.Text = directory.Title;
                verticalColumn.Children.Add(buttonChapter);
            }

            StackLayout AddLayout = new StackLayout();
            AddLayout.HorizontalOptions = LayoutOptions.Center;
            AddLayout.VerticalOptions = LayoutOptions.EndAndExpand;

            Button add = new Button();
            add.Text = "+";
            add.FontSize = FontSize * divisor; ;
            add.Clicked += OnAddChapterClicked;
            add.CornerRadius = 10;
            add.Padding = 15;
            add.Margin = 10/divisor;
            add.BackgroundColor = Color.White;
            AddLayout.Children.Add(add);

            Button remove = new Button() { Text = "-" };
            remove.Clicked += OnRemoveClicked;
            remove.Padding = 15;
            remove.FontSize = FontSize * divisor;
            remove.CornerRadius = 10;
            remove.Margin = 10/divisor;
            remove.BackgroundColor = Color.White;
            AddLayout.Children.Add(remove);

            Button settings = new Button() { Text = "⋮" };
            settings.BackgroundColor = Color.White;
            settings.CornerRadius = 10;
            settings.Padding = 15;
            settings.Margin = 10/divisor;
            settings.Clicked += OnSettingsPageCliked;
            settings.FontSize = FontSize * divisor; ;
            AddLayout.Children.Add(settings);

            Button scan = new Button() { Text = "*"};
            scan.BackgroundColor = Color.White;
            scan.CornerRadius = 10;
            scan.Padding = 15;
            scan.Margin = 10 / divisor;
            scan.Clicked += OnScannerClicked;
            scan.FontSize = fontsize;
            AddLayout.Children.Add(scan);

            stackNotes = new StackLayout();
            stackNotes.Margin = 15/divisor;
            stackNotes.HorizontalOptions = LayoutOptions.CenterAndExpand;

            if (directories.Length > 0)
            {
                StackLayout stackListNotes = new StackLayout();
                stackListNotes.Margin = 15/divisor;
                stackListNotes.VerticalOptions = LayoutOptions.CenterAndExpand;
                stackListNotes.HorizontalOptions = LayoutOptions.FillAndExpand;
                stackListNotes.Children.Clear();
                currentDirectory = directories[0].Id;
                Note[] notes = Note.GetNotesByIdDirectory(directories[0].Id);
                if (notes.Length == 0)
                {
                    Label pleaseAddChapter = new Label();
                    pleaseAddChapter.Text = "Vous n'avez pas encore de notes. \nVous pouvez en créer un en utilisant le bouton ci-dessous";
                    pleaseAddChapter.Padding = 100;
                    stackNotes.Children.Add(pleaseAddChapter);
                }
                else
                {
                    foreach (Note note in notes)
                    {
                        Button temp = new Button();
                        temp.FontSize = fontsize;
                        temp.CornerRadius = 25;
                        temp.BorderWidth = 1;
                        temp.Text = note.Title;
                        //temp.BackgroundColor = Color.White;
                        temp.FontAttributes = FontAttributes.Italic;
                        temp.Clicked += OnEditorClicked;
                        stackListNotes.Children.Add(temp);
                    }
                }

                ScrollView scrollNotes = new ScrollView();
                scrollNotes.Content = stackListNotes;
                stackNotes.Children.Add(scrollNotes);

                StackLayout allButtonList = new StackLayout();
                allButtonList.VerticalOptions = LayoutOptions.EndAndExpand;
                allButtonList.Margin = 20 / divisor;

                StackLayout buttonListNotes = new StackLayout();
                buttonListNotes.Orientation = StackOrientation.Horizontal;
                

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

                buttonListNotes.Children.Add(removeNote);

                allButtonList.Children.Add(buttonListNotes);

                Button share = new Button();
                share.Text = "Partager";
                share.BackgroundColor = Color.White;
                share.Clicked += OnShareClicked;
                share.FontSize = fontsize;

                allButtonList.Children.Add(share);

                Button QRbutton = new Button();
                QRbutton.Text = "Générer";
                QRbutton.BackgroundColor = Color.White;
                QRbutton.Clicked += OnQRCodeClicked;
                QRbutton.FontSize = fontsize;

                allButtonList.Children.Add(QRbutton);

                if (Device.RuntimePlatform == Device.UWP)
                {
                    AddNote.Text = "Ajouter une note";
                    removeNote.Text = "Retirer une note";
                    share.Text = "Partager ce chapitre";
                    QRbutton.Text = "Générer un QR Code";
                }
               

                if (notes.Length == 0) 
                {
                    removeNote.IsEnabled = false;
                }

                

                stackNotes.Children.Add(allButtonList);
            }
            else
            {
                Label pleaseAddChapter = new Label();
                pleaseAddChapter.Text = "Vous n'avez pas encore de chapitre. Vous pouvez en créer un en utilisant le bouton +";
                pleaseAddChapter.Padding = 100;
                stackNotes.Children.Add(pleaseAddChapter);
            }

            ScrollView scrollChapter = new ScrollView();
            scrollChapter.Content = verticalColumn;

            verticalLayout.Children.Add(scrollChapter);
            verticalLayout.Children.Add(AddLayout);
            mainContent.Children.Add(verticalLayout);
            mainContent.Children.Add(stackNotes);

            this.Content = mainContent;
            this.BackgroundColor = user.ColorTheme;
        }

        private void OnEditorClicked(object sender, EventArgs e)
        {
            var page = new EditorPage(Note.GetNoteByTitleAndIdDirectory(((Button)sender).Text, currentDirectory), user);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        public void OnShareClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShareChapterPage(user, currentDirectory));
        }

        public void OnScannerClicked(object sender, EventArgs e)
        {
            var scan = new ScannerPage(user);
            Navigation.PushAsync(scan);
          
            scan.OnScanResult += (result) => Device.BeginInvokeOnMainThread(() =>
            {
                var sort = result.Text;
                
                var split = sort.Split('/');

                int directoryID = Int32.Parse(split[1]);

                string ownername = split[0];

                var owner = User.GetUserByUsername(ownername);

                Directory.ShareDirectory(directoryID, owner, user.Username);

                scan.DisplayAlert("QR code trouvé", owner.Username + " vous a partagé un chapitre", "OK");
                Navigation.PopAsync();
            }); 
        }

        public void OnQRCodeClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QRCodeGeneratorPage(user, currentDirectory));
        }

        

        private void OnNoteClicked(object sender, EventArgs e)
        {
            stackNotes.Children.Clear();
            currentDirectory = Directory.GetDirectoryByTitleAndIdOwner(((Button)sender).Text, user).Id;
            Note[] notes = Note.GetNotesByIdDirectory(currentDirectory);
            StackLayout stackListNotes = new StackLayout();
            stackListNotes.Margin = 15 / divisor;
            stackListNotes.VerticalOptions = LayoutOptions.CenterAndExpand;
            stackListNotes.HorizontalOptions = LayoutOptions.FillAndExpand;
            stackListNotes.Children.Clear();

            if (notes.Length == 0)
            {
                Label pleaseAddChapter = new Label();
                pleaseAddChapter.Text = "Vous n'avez pas encore de notes.\nVous pouvez en créer un en utilisant le bouton ci-dessous";
                pleaseAddChapter.Padding = 100;
                stackNotes.Children.Add(pleaseAddChapter);
            }
            else
            {
                foreach (Note note in notes)
                {
                    Button temp = new Button();
                    temp.FontSize = fontsize;
                    temp.BorderWidth = 1;
                    temp.Text = note.Title;
                    temp.CornerRadius = 25;
                    temp.FontAttributes = FontAttributes.Italic;
                    temp.Clicked += OnEditorClicked;
                    stackListNotes.Children.Add(temp);
                }
            }
            ScrollView scrollNotes = new ScrollView();
            scrollNotes.Content = stackListNotes;
            stackNotes.Children.Add(scrollNotes);

            StackLayout allButtonList = new StackLayout();
            allButtonList.VerticalOptions = LayoutOptions.EndAndExpand;
            allButtonList.Margin = 20 / divisor;

            StackLayout buttonListNotes = new StackLayout();
            buttonListNotes.Orientation = StackOrientation.Horizontal;
            buttonListNotes.VerticalOptions = LayoutOptions.EndAndExpand;
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

            allButtonList.Children.Add(buttonListNotes);

            Button share = new Button();
            share.Text = "Partager";
            share.BackgroundColor = Color.White;
            share.Clicked += OnShareClicked;
            share.FontSize = fontsize;

            allButtonList.Children.Add(share);

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
            stackNotes.Children.Add(allButtonList);
        }
        private void OnAddNotePageClicked(object sender, EventArgs e)
        {
            int noteMax = 12;
            if (Note.GetNotesByIdDirectory(currentDirectory).Length > noteMax && !user.Premium)
            {
                DisplayAlert("Option Premium", $"Vous ne pouvez pas créer plus de {noteMax} notes sans l'option Premium", "Ok");
                return;
            }
            string text = ((Button)sender).Text;
            string[] splited = text.Split(' ');
            var page = new AddNotePage(user, Directory.GetDirectoryByIdAndIdOwner(currentDirectory, user));
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
            int chapMax = 8;
            if (Directory.GetUserDirectories(user).Length > chapMax && !user.Premium)
            {
                DisplayAlert("Option Premium", $"Vous ne pouvez pas créer plus de {chapMax} chapitres sans l'option Premium", "Ok");
                return;
            }
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

        private void SortList(Directory[] rep)
        {
            for (int i = 0; i < rep.Length; i++)
            {
                for (int j = i + 1; j < rep.Length; j++)
                    if (String.Compare(rep[j].Title, rep[i].Title, true) == -1)
                        (rep[i], rep[j]) = (rep[j], rep[i]);
            }
        }
    }
}