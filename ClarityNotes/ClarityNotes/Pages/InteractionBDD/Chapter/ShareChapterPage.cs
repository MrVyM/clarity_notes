using System;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class ShareChapterPage : ContentPage
    {

        User user;
        Entry peopleToShare;
        int idDirectory;
        public ShareChapterPage(User user, int idDirectory)
        {
            this.user = user;
            this.idDirectory = idDirectory;

            StackLayout mainContent = new StackLayout();
            mainContent.Padding = 50;
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.CenterAndExpand;

            Frame containScroll = new Frame();
            containScroll.BackgroundColor = Color.White;
            Label label1 = new Label();
            label1.Text = "Utilisateurs ayant l'accès au chapitre :\n";
            StackLayout container = new StackLayout();
            container.Children.Add(label1);
            ScrollView scrollListShare = new ScrollView();
            StackLayout listShare = new StackLayout();
            listShare.BackgroundColor = user.ColorTheme;
            User[] listUsers = Directory.GetUsersByDirectory(idDirectory);
            User root = listUsers[0];
            if (user.Email == root.Email)
            {
                foreach (User share in listUsers)
                {
                    StackLayout peopleButton = new StackLayout();
                    peopleButton.Orientation = StackOrientation.Horizontal;

                    Label people = new Label();
                    people.Text = ((share == root) ? "♛ " : "   ") + share.Username + "     " + share.Email;
                    people.VerticalOptions = LayoutOptions.CenterAndExpand;
                    people.FontSize = 15;
                    people.Margin = 5;
                    people.Padding = 10;

                    Button param = new Button();
                    param.Text = "⋮";
                    param.FontSize = 15;
                    param.CornerRadius = 5;
                    param.Margin = 15;
                    param.BackgroundColor = Color.White;
                    param.Scale = (Device.RuntimePlatform == Device.UWP ? 1 : 0.8);
                    param.HorizontalOptions = LayoutOptions.End;
                    param.VerticalOptions = LayoutOptions.Center;
                    param.Clicked += (sender, e) =>
                    {
                        OnSettingsClicked(share);
                    };
                    peopleButton.Children.Add(people);
                    if (share.Email != root.Email)
                        peopleButton.Children.Add(param);

                    listShare.Children.Add(peopleButton);

                }
            }
            else
            {
                foreach (User share in listUsers)
                {
                    Label people = new Label();
                    people.Text = ((share == root) ? "♛ " : "   ") + share.Username + "     " + share.Email;
                    people.HorizontalOptions = LayoutOptions.Center;
                    people.VerticalOptions = LayoutOptions.CenterAndExpand;
                    people.FontSize = 15;
                    people.Padding = 10;
                    people.Margin = 5;
                    listShare.Children.Add(people);
                }
            }
            scrollListShare.Content = listShare;
            container.Children.Add(scrollListShare);

            containScroll.Content = container;


            mainContent.Children.Add(containScroll);

            if (user.Email == root.Email)
            {
                Frame frame = new Frame();
                frame.HasShadow = true;
                frame.HorizontalOptions = LayoutOptions.Center;
                frame.BackgroundColor = Color.White;

                StackLayout framStack = new StackLayout();

                Label label = new Label();
                label.FontSize = 16;
                label.Text = $"Veuillez entrer l'email ou le nom d'utilisateur de la cible :";
                framStack.Children.Add(label);

                peopleToShare = new Entry();
                peopleToShare.Completed += OnSubmitClicked;
                framStack.Children.Add(peopleToShare);

                Button submit = new Button();
                submit.HorizontalOptions = LayoutOptions.Center;
                submit.VerticalOptions = LayoutOptions.Center;
                submit.Clicked += OnSubmitClicked;
                submit.Margin = 20;
                submit.Text = "Valider";

                frame.Content = framStack;
                mainContent.Children.Add(frame);
                mainContent.Children.Add(submit);
            }

            this.Content = mainContent;
            this.BackgroundColor = user.ColorTheme;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (peopleToShare.Text == user.Username || peopleToShare.Text == user.Email)
                DisplayAlert("Erreur", "Vous ne pouvez pas vous partager une note.", "OK");
            else if (!Directory.ShareDirectory(idDirectory, user, peopleToShare.Text))
                DisplayAlert("Erreur", "L'utilisateur est introuvable.", "OK");
            else
            {
                var page = new RootPage(user);
                NavigationPage.SetHasNavigationBar(page, false);
            }
        }

        private void OnSettingsClicked(User currentUser)
        {
            var page = new ShareSettingsPage(user, currentUser, idDirectory);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
    }
}