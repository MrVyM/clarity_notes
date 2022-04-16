using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class CreateAccountPage : ContentPage
    {
        Entry pass1Entry;
        Entry pass2Entry;
        Entry emailEntry;
        Entry nameEntry;
        Button create;
        public CreateAccountPage()
        {
            StackLayout content = new StackLayout();
            content.VerticalOptions = LayoutOptions.CenterAndExpand;
            content.Padding = 50;



            Frame email = new Frame();
            email.BorderColor = Color.Gray;
            email.BackgroundColor = Color.White;
            email.Margin = 20;
            email.CornerRadius = 10;

            StackLayout emailStack = new StackLayout();

            Label emailLabel = new Label();
            emailLabel.Text = "Votre email :";
            emailStack.Children.Add(emailLabel);   

            emailEntry = new Entry();
            emailStack.Children.Add(emailEntry);
        

            email.Content = emailStack;
            content.Children.Add(email);

            Frame username = new Frame();
            username.BorderColor = Color.Gray;
            username.BackgroundColor = Color.White;
            username.Margin = 20;
            username.CornerRadius = 10;

            StackLayout userStack = new StackLayout();

            Label userLabel = new Label();
            userLabel.Text = "Votre nom :";
            userStack.Children.Add(userLabel);

            nameEntry = new Entry();
            userStack.Children.Add(nameEntry);


            username.Content = userStack;

            content.Children.Add(username);

            Frame password = new Frame();
            password.Margin = 20;
            password.BorderColor = Color.Gray;
            password.BackgroundColor = Color.White;
            password.CornerRadius = 10;

            StackLayout passStack = new StackLayout();

            Label passWordLabel = new Label();
            passWordLabel.Text = "Votre mot de passe : ";
            passStack.Children.Add(passWordLabel);

            pass1Entry = new Entry();
            pass1Entry.IsPassword = true;
            passStack.Children.Add(pass1Entry);

            Label passNewLabel = new Label();
            passNewLabel.Text = "De nouveau, votre mot de passe :";
            passStack.Children.Add(passNewLabel);

            pass2Entry = new Entry();
            pass2Entry.IsPassword = true;
            pass2Entry.TextChanged += OnCompare;
            passStack.Children.Add(pass2Entry);

            password.Content = passStack;
            content.Children.Add(password);


            create = new Button();
            create.IsEnabled = false;
            create.Text = "Créer";
            create.HorizontalOptions = LayoutOptions.Center;
            create.VerticalOptions = LayoutOptions.Center;

            create.Clicked += OnCreateCliked;

            content.Children.Add(create);
            this.BackgroundColor = Color.Beige;
            this.Content = content; 
        }


        public void OnCompare(object sender, EventArgs e)
        {
            bool condition = pass1Entry.Text == pass2Entry.Text && pass1Entry.Text.Length >= 6;
            if (!condition)
            {
                create.IsEnabled = false;
                pass1Entry.TextColor = Color.Red;
                pass2Entry.TextColor = Color.Red;
            }
            else
            {
                create.IsEnabled = true;
                pass1Entry.TextColor= Color.Blue;
                pass2Entry.TextColor= Color.Blue;
            }
        }

        private void OnCreateCliked(object sender, EventArgs e)
        {
            
        }
    }
}