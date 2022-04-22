﻿using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class CreateAccountPage : ContentPage
    {
        private Entry usernameEntry;
        private Entry emailEntry;
        private Entry passwordEntry;
        private Entry passwordConfirmEntry;
        private Button confirmButton;

        public CreateAccountPage()
        {
            StackLayout content = new StackLayout();
            content.VerticalOptions = LayoutOptions.CenterAndExpand;
            content.Padding = 50;

            Frame email = new Frame();
            email.BackgroundColor = Color.White;
            email.Margin = 20;
            email.CornerRadius = 10;

            StackLayout emailStack = new StackLayout();

            Label emailLabel = new Label();
            emailLabel.Text = "Insérez votre adresse mail :";
            emailStack.Children.Add(emailLabel);

            emailEntry = new Entry();
            emailStack.Children.Add(emailEntry);


            email.Content = emailStack;
            content.Children.Add(email);

            Frame username = new Frame();
            username.BackgroundColor = Color.White;
            username.Margin = 20;
            username.CornerRadius = 10;

            StackLayout userStack = new StackLayout();

            Label userLabel = new Label();
            userLabel.Text = "Choisissez un nom d'utilisateur :";
            userStack.Children.Add(userLabel);

            usernameEntry = new Entry();
            userStack.Children.Add(usernameEntry);

            username.Content = userStack;

            content.Children.Add(username);

            Frame password = new Frame();
            password.Margin = 20;
            password.BackgroundColor = Color.White;
            password.CornerRadius = 10;

            StackLayout passStack = new StackLayout();

            Label passWordLabel = new Label();
            passWordLabel.Text = "Choisissez votre mot de passe :";
            passStack.Children.Add(passWordLabel);

            passwordEntry = new Entry();
            passwordEntry.IsPassword = true;
            passStack.Children.Add(passwordEntry);

            Label passNewLabel = new Label();
            passNewLabel.Text = "Confirmez votre mot de passe :";
            passStack.Children.Add(passNewLabel);

            passwordConfirmEntry = new Entry();
            passwordConfirmEntry.IsPassword = true;
            passwordConfirmEntry.TextChanged += OnCompare;
            passStack.Children.Add(passwordConfirmEntry);

            password.Content = passStack;
            content.Children.Add(password);


            confirmButton = new Button();
            confirmButton.IsEnabled = false;
            confirmButton.Text = "S'inscrire";
            confirmButton.HorizontalOptions = LayoutOptions.Center;
            confirmButton.VerticalOptions = LayoutOptions.Center;

            confirmButton.Clicked += OnCreateClicked;

            content.Children.Add(confirmButton);
            BackgroundColor = Color.Beige;
            Content = content;

            usernameEntry.Placeholder = "Username";
            emailEntry.Placeholder = "Email";
            passwordEntry.Placeholder = "Password";
            passwordConfirmEntry.Placeholder = "Password";
        }

        public void OnCompare(object sender, EventArgs e)
        {
            bool condition = passwordEntry.Text == passwordConfirmEntry.Text && passwordEntry.Text.Length >= 6;
            if (!condition)
            {
                confirmButton.IsEnabled = false;
                passwordEntry.TextColor = Color.Red;
                passwordConfirmEntry.TextColor = Color.Red;
            }
            else
            {
                confirmButton.IsEnabled = true;
                passwordEntry.TextColor = Color.Blue;
                passwordConfirmEntry.TextColor = Color.Blue;
            }
        }

        private void OnCreateClicked(object sender, EventArgs e)
        {
            bool result = User.CreateUser(emailEntry.Text, usernameEntry.Text, passwordEntry.Text);
            if (result) Navigation.PopAsync();
            else
            {
                usernameEntry.Text = "";
                emailEntry.Text = "";
                passwordEntry.Text = "";
                passwordConfirmEntry.Text = "";
                DisplayAlert("Inscription échouée", "Un compte possède déjà ce nom d'utilisateur ou cette email.", "OK");
            }
        }
    }
}