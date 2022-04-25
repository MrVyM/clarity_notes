using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class NewMailPage : ContentPage
    {
        User user;
        Entry mailEntry;
        public NewMailPage(User user)
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
            label.Text = "Veuillez entrer votre nouveau mail";
            framStack.Children.Add(label);

            mailEntry = new Entry();
            mailEntry.IsPassword = true;
            framStack.Children.Add(mailEntry);

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
            User.Change(user.Id, "email", mailEntry.Text);
            Navigation.PopToRootAsync();
        }
    }
}