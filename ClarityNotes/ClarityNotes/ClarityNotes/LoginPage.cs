using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class LoginPage : ContentPage
    {
        Entry idEntry;
        Entry mdpEntry;
        public LoginPage()
        {

            StackLayout mainContent = new StackLayout();
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.Center;

            Frame idFrame = new Frame();
            idFrame.Margin = 15;
            StackLayout idStack = new StackLayout();

            Label idLabel = new Label();
            idLabel.Text = "Votre identifiant : ";
            idStack.Children.Add(idLabel);

            idEntry = new Entry();
            idEntry.WidthRequest = 100;
            idStack.Children.Add(idEntry);

            idFrame.Content = idStack;


            Frame mdpFrame = new Frame();
            mdpFrame.Margin = 25;
            StackLayout mdpStack = new StackLayout();

            Label mdpLabel = new Label();
            mdpLabel.Text = "Votre mot de passe : ";
            mdpStack.Children.Add(mdpLabel);

            mdpEntry = new Entry();
            mdpEntry.IsPassword = true;
            mdpEntry.WidthRequest = 100;
            mdpStack.Children.Add(mdpEntry);

            mdpFrame.Content = mdpStack;

            Button connexion = new Button();
            connexion.Text = "Connexion";
            connexion.Clicked += OnConnexionCliked;

            
            mainContent.Children.Add(idFrame);
            mainContent.Children.Add(mdpFrame);
            mainContent.Children.Add(connexion);
            this.Content = mainContent;
               
        }

        private void OnConnexionCliked(object sender, EventArgs e)
        {
            var mdpEntryText = mdpEntry.Text;
            mdpEntry.Text = "";
            if (idEntry.Text == "root" && mdpEntryText == "toor")
                Console.WriteLine("yep");
        
        }
    }
}