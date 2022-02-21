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
        Label error;
        public LoginPage()
        { 

            StackLayout mainContent = new StackLayout();
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.Center;
            mainContent.WidthRequest = 500;

            Frame idFrame = new Frame();
            idFrame.Margin = 25;
            StackLayout idStack = new StackLayout();

            error = new Label();
            error.FontSize = 18;
            error.TextColor = Color.Red;
            error.HorizontalOptions = LayoutOptions.Center;
            error.VerticalOptions = LayoutOptions.Center;   
            mainContent.Children.Add(error);

            Label idLabel = new Label();
            idLabel.Text = "Votre identifiant : ";
            idStack.Children.Add(idLabel);

            idEntry = new Entry();
            idStack.Children.Add(idEntry);

            idFrame.Content = idStack;


            Frame mdpFrame = new Frame();
            mdpFrame.Margin = 25;
            mdpFrame.HorizontalOptions = LayoutOptions.FillAndExpand;
            StackLayout mdpStack = new StackLayout();

            Label mdpLabel = new Label();
            mdpLabel.Text = "Votre mot de passe : ";
            mdpStack.Children.Add(mdpLabel);

            mdpEntry = new Entry();
            mdpEntry.IsPassword = true;
            mdpStack.Children.Add(mdpEntry);

            mdpFrame.Content = mdpStack;

            Button connexion = new Button();
            connexion.Text = "Connexion";
            connexion.HorizontalOptions = LayoutOptions.Center;
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
            error.Text = "";
            System.Threading.Thread.Sleep(2000);
            if (idEntry.Text == "root" && mdpEntryText == "toor")
            {
                Navigation.PopAsync();
                var page = new RootPage();
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
                
            }
            else 
            {
                error.Text = "Identifiant ou mot de passe invalid.";
            }
        }
    }
}