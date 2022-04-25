using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class NewNamePage : ContentPage
    {
        public NewNamePage(User user)
        {
            StackLayout mainContent = new StackLayout();
            Label texte = new Label();
            texte.Text = "NAME";
            mainContent.Children.Add(texte);
            this.Content = mainContent;
        }
    }
}