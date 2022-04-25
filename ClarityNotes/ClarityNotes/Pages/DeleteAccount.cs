using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class DeleteAccount : ContentPage
    {
        Entry password;
        User user;
        public DeleteAccount(User user)
        {
            this.user = user;
            StackLayout mainContent = new StackLayout();
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.Center;

            Frame frame = new Frame();
            StackLayout framStack = new StackLayout();
            
            Label texte = new Label();
            texte.Text = "Veuillez entrer votre mot de passe";
            framStack.Children.Add(texte);

            password = new Entry();
            password.IsPassword = true;
            framStack.Children.Add(password);

            frame.Content = framStack;
            mainContent.Children.Add(frame);

            Button delete = new Button();
            delete.Text = "Suprimer";
            delete.Clicked += OnDelete;
            mainContent.Children.Add(delete);


            this.BackgroundColor = Color.FromHex("57b1eb");
            this.Title = "Supression de compte";
            this.Content = mainContent;
        }
        private void OnDelete(object sender, EventArgs e)
        {
            User.DeleteUser(user.Id);
            Navigation.PopToRootAsync();
        }
    }
}