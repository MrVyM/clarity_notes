using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class NewPasswordPage : ContentPage
    {
        public NewPasswordPage(User user)
        {
            StackLayout mainContent = new StackLayout();
            Label texte = new Label();
            texte.Text = "PASSWORD";
            mainContent.Children.Add(texte);
            this.Content = mainContent;
        }
    }
}