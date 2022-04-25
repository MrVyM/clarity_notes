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
            frame.BackgroundColor = Color.Beige;
            frame.HorizontalOptions = LayoutOptions.Center;
            StackLayout framStack = new StackLayout();

            Label label = new Label();
            label.FontSize = 16;
            label.Text = "Veuillez entrer votre nouveau nom d'utilisateur";
            framStack.Children.Add(label);

            nameEntry = new Entry();
            nameEntry.IsPassword = true;
            framStack.Children.Add(nameEntry);

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
            User.Change(user.Id, "name", nameEntry.Text);
            OnLogOut(new object(), new EventArgs());
        }

        private void OnLogOut(Object sender, EventArgs e)
        {
            foreach (var page in Navigation.ModalStack)
            {
                if (page != this)
                    Navigation.RemovePage(page);
            }
            Navigation.PushAsync(new LoginPage());
            Navigation.RemovePage(this);
        }
    }
}