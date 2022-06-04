using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            label1.Text = "Personne possedant les acces :";
            StackLayout container = new StackLayout();
            container.Children.Add(label1);
            ScrollView scrollListShare = new ScrollView();
            StackLayout listShare = new StackLayout();
            foreach (User share in Directory.GetUsersByDirectory(idDirectory))
            {
                Label people = new Label();
                people.Text = share.Username +" "+share.Email;
                people.HorizontalOptions = LayoutOptions.Center;
                people.VerticalOptions = LayoutOptions.CenterAndExpand;
                people.FontSize = 15;
                listShare.Children.Add(people);
            }

            scrollListShare.Content = listShare;
            container.Children.Add(scrollListShare);

            containScroll.Content = container;


            mainContent.Children.Add(containScroll);


            Frame frame = new Frame();
            frame.HasShadow = true;
            frame.HorizontalOptions = LayoutOptions.Center;
            frame.BackgroundColor = Color.White;

            StackLayout framStack = new StackLayout();

            Label label = new Label();
            label.FontSize = 16;
            label.Text = $"Veuillez entrer l'email ou le pseudo de la personne :";
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

            this.Content = mainContent;
            this.BackgroundColor = user.ColorTheme;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (!Directory.ShareDirectory(idDirectory,user, peopleToShare.Text))
                DisplayAlert("Erreur", "Personne Introuvable", "OK");
            else
            {
                var page = new RootPage(user);
                NavigationPage.SetHasNavigationBar(page, false);
            }
        }
    }
}