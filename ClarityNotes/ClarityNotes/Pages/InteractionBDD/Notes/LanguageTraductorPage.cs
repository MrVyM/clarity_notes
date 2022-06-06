using System;
using System.Collections.Generic;

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
                Title = "Sélection de la langue de traduction",
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
            languageCode.Add("Afrikaans", "af");
            languageCode.Add("Albanais", "sq");
            languageCode.Add("Amharique", "am");
            languageCode.Add("Arabe", "ar");
            languageCode.Add("Arménien", "hy");
            languageCode.Add("Azéri", "az");
            languageCode.Add("Basque", "eu");
            languageCode.Add("Biélorusse", "be");
            languageCode.Add("Bengali", "bn");
            languageCode.Add("Bosnien", "bs");
            languageCode.Add("Bulgare", "bg");
            languageCode.Add("Catalan", "ca");
            languageCode.Add("Cebuano", "ceb");
            languageCode.Add("Chinois", "zh");
            languageCode.Add("Corse", "co");
            languageCode.Add("Croate", "hr");
            languageCode.Add("Tchèque", "cs");
            languageCode.Add("Danois", "da");
            languageCode.Add("Néerlandais", "nl");
            languageCode.Add("Anglais", "en");
            languageCode.Add("Espéranto", "eo");
            languageCode.Add("Estonien", "et");
            languageCode.Add("Finnois", "fi");
            languageCode.Add("Français", "fr");
            languageCode.Add("Frison", "fy");
            languageCode.Add("Galicien", "gl");
            languageCode.Add("Géorgien", "ka");
            languageCode.Add("Allemand", "de");
            languageCode.Add("Grec", "el");
            languageCode.Add("Gujarati", "gu");
            languageCode.Add("Créole", "ht");
            languageCode.Add("Haoussa", "ha");
            languageCode.Add("Hawaïen", "haw");
            languageCode.Add("Hébreuhe", "iw");
            languageCode.Add("Hindi", "hi");
            languageCode.Add("Hmong", "hmn");
            languageCode.Add("Hongrois", "hu");
            languageCode.Add("Islandais", "is");
            languageCode.Add("Igbo", "ig");
            languageCode.Add("Indonésien", "id");
            languageCode.Add("Irlandais", "ga");
            languageCode.Add("Italien", "it");
            languageCode.Add("Japonais", "ja");
            languageCode.Add("Javanais", "jv");
            languageCode.Add("Kannada", "kn");
            languageCode.Add("Kazakh", "kk");
            languageCode.Add("Khmer", "km");
            languageCode.Add("Kinyarwanda", "rw");
            languageCode.Add("Coréen", "ko");
            languageCode.Add("Kurde", "ku");
            languageCode.Add("Kirghyz", "ky");
            languageCode.Add("Laotien", "lo");
            languageCode.Add("Latin", "la");
            languageCode.Add("Letton", "lv");
            languageCode.Add("Lituanien", "lt");
            languageCode.Add("Luxembourgeois", "lb");
            languageCode.Add("Macédonien", "mk");
            languageCode.Add("Malgache", "mg");
            languageCode.Add("Malais", "ms");
            languageCode.Add("Malayâlam", "ml");
            languageCode.Add("Maltais", "mt");
            languageCode.Add("Maori", "mi");
            languageCode.Add("Marathi", "mr");
            languageCode.Add("Mongol", "mn");
            languageCode.Add("Birman", "my");
            languageCode.Add("Népalais", "ne");
            languageCode.Add("Norvégien", "no");
            languageCode.Add("Nyanja", "ny");
            languageCode.Add("Odia", "or");
            languageCode.Add("Pachtô", "ps");
            languageCode.Add("Perse", "fa");
            languageCode.Add("Polonais", "pl");
            languageCode.Add("Portugais", "pt");
            languageCode.Add("Panjabi", "pa");
            languageCode.Add("Roumain", "ro");
            languageCode.Add("Russe", "ru");
            languageCode.Add("Samoan", "sm");
            languageCode.Add("Gaélique", "gd");
            languageCode.Add("Serbe", "sr");
            languageCode.Add("Sesotho", "st");
            languageCode.Add("Shona", "sn");
            languageCode.Add("Sindhî", "sd");
            languageCode.Add("Singhalais", "si");
            languageCode.Add("Slovaque", "sk");
            languageCode.Add("Slovène", "sl");
            languageCode.Add("Somali", "so");
            languageCode.Add("Spanish", "es");
            languageCode.Add("Soundanais", "su");
            languageCode.Add("Swahili", "sw");
            languageCode.Add("Suédois", "sv");
            languageCode.Add("Tagalog", "tl");
            languageCode.Add("Tadjik", "tg");
            languageCode.Add("Tamoul", "ta");
            languageCode.Add("Tatar", "tt");
            languageCode.Add("Télougou", "te");
            languageCode.Add("Thaï", "th");
            languageCode.Add("Turc", "tr");
            languageCode.Add("Turkmène", "tk");
            languageCode.Add("Ukrainien", "uk");
            languageCode.Add("Urdu", "ur");
            languageCode.Add("Ouïghour", "ug");
            languageCode.Add("Ouzbek", "uz");
            languageCode.Add("Vietnamien", "vi");
            languageCode.Add("Gallois", "cy");
            languageCode.Add("Xhosa", "xh");
            languageCode.Add("Yiddish", "yi");
            languageCode.Add("Yoruba", "yo");
            languageCode.Add("Zulu", "zu");

            return languageCode;
        }
    }
}