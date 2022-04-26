using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class NewNamePage : ContentPage
    {
        User user;
        Entry nameEntry;
        public NewNamePage(User user)
        {
            this.user = user;

            StackLayout mainContent = new StackLayout();
            mainContent.Padding = 50;
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.CenterAndExpand;

            Frame frame = new Frame();
            frame.HasShadow = true;
            frame.BackgroundColor = Color.FromHex("94c6ff");
            frame.HorizontalOptions = LayoutOptions.Center;

            StackLayout framStack = new StackLayout();

            Label label = new Label();
            label.FontSize = 16;
            label.Text = "Veuillez entrer votre nouveau nom d'utilisateur";
            framStack.Children.Add(label);

            nameEntry = new Entry();
            framStack.Children.Add(nameEntry);

            Button submit = new Button();
            submit.HorizontalOptions = LayoutOptions.Center;
            submit.VerticalOptions = LayoutOptions.Center;
            submit.BackgroundColor = Color.FromHex("94c6ff");
            submit.Clicked += OnSubmitClicked;
            submit.Text = "Valider";

            frame.Content = framStack;
            mainContent.Children.Add(frame);
            mainContent.Children.Add(submit);

            this.Content = mainContent;
            this.BackgroundColor = Color.FromHex("57b1eb");
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            User.Change(user.Id, "username", nameEntry.Text);
            Navigation.PopToRootAsync();
        }
    }
}