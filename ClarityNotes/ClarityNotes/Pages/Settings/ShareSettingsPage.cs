using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class ShareSettingsPage : ContentPage
    {
        User target;
        Picker picker;
        User user;
        int idDirectory;
        public ShareSettingsPage(User user, User target,int idDirectory)
        {
            this.target = target;
            this.idDirectory = idDirectory;
            this.user = user;

            StackLayout mainContent = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 500
            };

            StackLayout stackLayout = new StackLayout();

            Label questionLabel = new Label()
            {
                FontSize = 16,
                Text = $"Options de partage avec l'utilisateur {target.Username.ToUpper()} \n" +
                $"l'adresse mail de l'utilisateur est {target.Email} \n",
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center
            };

            picker = new Picker
            {
                Title = "Sélection de l'action",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string colorName in new string[] 
            { 
                "Retirer le partage de l'utilisateur", 
                "Donner la propriété",
                (Directory.GetReadOnlyByDirectoryAndIdOwner(idDirectory,target) ? "Accorder l'ecriture" : "Accorder seulement la lecture")
            })
                picker.Items.Add(colorName);

            Button submit = new Button()
            {
                Text = "Confirmation",
                HorizontalOptions = LayoutOptions.Center
            };
            submit.Clicked += OnSubmitClicked;

            stackLayout.Children.Add(questionLabel);
            stackLayout.Children.Add(picker);
            mainContent.Children.Add(stackLayout);
            mainContent.Children.Add(submit);

            this.Content = mainContent;
            this.BackgroundColor = user.ColorTheme;
        }
        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (picker.SelectedItem == null) return;
            switch (picker.SelectedItem.ToString())
            {
                case "Retirer le partage de l'utilisateur":
                    Directory.DeleteDirectory(idDirectory, target);
                    break;
                case "Donner la propriété":
                    Directory.ChangeRootOwner(user.Id, target.Id, idDirectory);
                    break;
                case "Accorder l'ecriture": 
                case "Accorder seulement la lecture" :
                    Directory.ChangeReadOnly(idDirectory, target);
                    break;
                default:
                    break;
            }
            var page = new RootPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
        }
    }
}