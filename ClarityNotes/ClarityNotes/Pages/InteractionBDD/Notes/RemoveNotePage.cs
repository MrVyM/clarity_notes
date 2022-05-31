using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class RemoveNotePage : ContentPage
    {
        private Picker picker;
        private User user;

        public RemoveNotePage(User user,int IdDirectory)
        {
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
                Text = "Quel note souhaitez-vous supprimer ?",
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center
            };

            picker = new Picker
            {
                Title = "Sélection de la note",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (Note n in Note.GetNotesByIdDirectory(IdDirectory))
                picker.Items.Add(n.Title);

            Button submit = new Button()
            {
                Text = "Confirmation",
                HorizontalOptions = LayoutOptions.Center
            };
            submit.Clicked += OnSubmitClicked;

            Label warningLabel = new Label()
            {
                FontSize = 12,
                Text = "Attention, la suppression d'une note est irréversible",
                TextColor = Color.Red,
                HorizontalTextAlignment = TextAlignment.Center
            };

            stackLayout.Children.Add(questionLabel);
            stackLayout.Children.Add(picker);
            stackLayout.Children.Add(warningLabel);

            mainContent.Children.Add(new Frame() { Margin = 25, Content = stackLayout });
            mainContent.Children.Add(submit);

            this.BackgroundColor = Color.FromHex("33B0FF");
            this.Content = mainContent;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (picker.SelectedItem == null) return;
            if (Note.DeleteNote(Note.GetNoteByTitle(picker.SelectedItem.ToString()).Id))
            {
                var page = new RootPage(user);
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page);
                Navigation.RemovePage(this);
            }
        }
    }
}