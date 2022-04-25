using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class NewMailPage : ContentPage
    {
        public NewMailPage(User user)
        {
            StackLayout mainContent = new StackLayout();
            Label texte = new Label();
            texte.Text = "MAIL";
            mainContent.Children.Add(texte);
            this.Content = mainContent;
        }
    }
}