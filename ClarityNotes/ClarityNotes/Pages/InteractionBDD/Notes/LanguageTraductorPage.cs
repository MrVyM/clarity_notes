using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class LanguageTraductorPage : ContentPage
    {
        private Picker picker;
        private User user;
        private int idNote;
        private EditorPage editorPage;

        public LanguageTraductorPage(EditorPage page, User user, int idNote)
        {
            this.user = user;
            this.idNote = idNote;
            this.editorPage = page;

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
                Text = "Dans quelle langue souhaitez-vous traduire la note ?",
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center
            };

            picker = new Picker
            {
                Title = "Sélection de la note",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string language in getLanguageCode().Keys)
                picker.Items.Add(language);

            Button submit = new Button()
            {
                Text = "Confirmation",
                HorizontalOptions = LayoutOptions.Center
            };
            submit.Clicked += OnSubmitClicked;

            Label warningLabel = new Label()
            {
                FontSize = 12,
                Text = "Attention, la traduction peut impacter la structure HTML de votre note.",
                TextColor = Color.Red,
                HorizontalTextAlignment = TextAlignment.Center
            };

            stackLayout.Children.Add(questionLabel);
            stackLayout.Children.Add(picker);
            stackLayout.Children.Add(warningLabel);

            mainContent.Children.Add(new Frame() { Margin = 25, Content = stackLayout });
            mainContent.Children.Add(submit);

            this.BackgroundColor = user.ColorTheme;
            this.Content = mainContent;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (picker.SelectedItem == null) return;
            Note note = Note.GetNoteById(idNote);
            string request = $"/translate?api-version=3.0&to={getLanguageCode()[picker.SelectedItem.ToString()]}";
            var rep = Traductor.Traduce(Traductor.SubscriptionKey, Traductor.Endpoint, request, note.Content);
            rep.Wait();
            note.Update(rep.Result, user);
            if (editorPage.editorAndroid == null)
                editorPage.editorWindows.HtmlText = note.Content;
            else
                editorPage.editorAndroid.Text = note.Content;
            Navigation.PopAsync();
        }

        private Dictionary<string, string> getLanguageCode()
        {
            Dictionary<string, string> languageCode = new Dictionary<string, string>();
            languageCode.Add("Allemand", "de");
            languageCode.Add("Anglais", "en");
            languageCode.Add("Espagnol", "es");
            languageCode.Add("Français", "fr");
            languageCode.Add("Italien", "it");
            return languageCode;
        }
    }
}