using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class NewPasswordPage : ContentPage
    {
        User user;
        Entry passwordEntry;
        public NewPasswordPage(User user)
        {
            this.user = user;

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
            label.Text = "Veuillez entrer votre nouveau mot de passe";
            framStack.Children.Add(label);

            passwordEntry = new Entry();
            passwordEntry.IsPassword = true;
            framStack.Children.Add(passwordEntry);

            Button submit = new Button();
            submit.HorizontalOptions = LayoutOptions.Center;
            submit.VerticalOptions = LayoutOptions.Center;
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
            User.Change(user.Id, "password", passwordEntry.Text);
            Navigation.PopToRootAsync();
        }
    }
}