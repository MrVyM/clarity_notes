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

            password = new Entry();
            password.IsPassword = true;
            mainContent.Children.Add(password);

            Button delete = new Button();
            delete.Text = "Suprimer";
            delete.Clicked += OnDelete;
            mainContent.Children.Add(delete);

            this.Title = "Supression de compte";
            this.Content = mainContent;
        }
        private void OnDelete(object sender, EventArgs e)
        {
            User.DeleteUser(user.Id);
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