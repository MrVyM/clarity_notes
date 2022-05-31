﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class GetPasswordPage : ContentPage
    {
        User user;
        Entry passwordEntry;

        public GetPasswordPage(User user, string change, string title = "")
        {
            this.user = user;

            StackLayout mainContent = new StackLayout();
            mainContent.Padding = 50;
            mainContent.HorizontalOptions = LayoutOptions.Center;
            mainContent.VerticalOptions = LayoutOptions.CenterAndExpand;

            Frame frame = new Frame();
            frame.HasShadow = true;
            frame.BackgroundColor = Color.FromHex("94c6ff");
            frame.HorizontalOptions = LayoutOptions.Center;

            StackLayout framStack = new StackLayout();

            Label label = new Label();
            label.FontSize = 16;
            label.Text = "Veuillez entrer votre mot de passe actuel";
            framStack.Children.Add(label);

            passwordEntry = new Entry();
            passwordEntry.IsPassword = true;
            framStack.Children.Add(passwordEntry);

            Button submit = new Button();
            submit.HorizontalOptions = LayoutOptions.Center;
            submit.BackgroundColor = Color.FromHex("94c6ff");
            submit.Margin = 20;
            submit.VerticalOptions = LayoutOptions.Center;

            switch (change) 
            {
                case "mail":
                    submit.Clicked += OnMailClicked;
                    break;
                case "password":
                    submit.Clicked += OnPasswordClicked;
                    break;
                case "name":
                    submit.Clicked += OnNameClicked;
                    break;
                default : 
                    {
                        Console.Error.WriteLine("GetPasswordPage : Wrong argument");
                        Environment.Exit(1);
                        break;
                    }
            }
            submit.Text = "Valider";

            frame.Content = framStack;
            mainContent.Children.Add(frame);
            mainContent.Children.Add(submit);

            if (title != "") this.Title = title;
            this.Content = mainContent;
            this.BackgroundColor = Color.FromHex("57b1eb");
        }

        private void OnMailClicked(object sender, EventArgs e)
        {
            string temp = passwordEntry.Text;
            passwordEntry.Text = "";
            if (user.Password != temp)
                Navigation.PopToRootAsync();
            else
            {
                var page = new NewMailPage(user);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
            }
        }
        private void OnPasswordClicked(object sender, EventArgs e)
        {
            string temp = passwordEntry.Text;
            passwordEntry.Text = "";
            if (user.Password != temp)
            {
                DisplayAlert("Erreur de mot de passe", "Nous vous avons déconnecté par mesure de précaution", "OK");
                Navigation.PopToRootAsync();
            }
            else
            {
                var page = new NewPasswordPage(user);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
            }
        }

        private void OnNameClicked(object sender, EventArgs e)
        {
            string temp = passwordEntry.Text;
            passwordEntry.Text = "";
            if (user.Password != temp)
                Navigation.PopToRootAsync();
            else
            {
                var page = new NewNamePage(user);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            User.DeleteUser(user.Id);
            Navigation.PopToRootAsync();
        }

    }
}