﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class AddNotePage : ContentPage
    {
        private User user;
        private Entry nameEntry;
        private Directory directory;

        public AddNotePage(User user, Directory directory)
        {
            this.user = user;
            this.directory = directory;

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
            label.Text = "Veuillez entrer un nom de note.";
            framStack.Children.Add(label);

            nameEntry = new Entry();
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
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (nameEntry.Text.Trim() == "")
            {
                var ans = DisplayAlert("Création impossible", "Ce nom n'est pas valide. Veuillez en choisir un autre.", "OK");
            }
            else if (Note.CreateNote(nameEntry.Text, directory.Id, user))
            {
                var page = new RootPage(user);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
                Navigation.RemovePage(this);
            }
            else
            {
                var ans = DisplayAlert("Création impossible", "Une note existe déjà sous ce nom.", "OK");
            }

        }
    }
}