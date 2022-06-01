using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.XForms.RichTextEditor;
using Google.Cloud.Translation.V2;
using Google.Api.Gax.ResourceNames;

namespace ClarityNotes
{
    public class EditorPage : ContentPage
    {
        Note note;
        User user;
        SfRichTextEditor editor;

<<<<<<< HEAD
=======
        public SfRichTextEditor _editor => editor;


>>>>>>> 2a99fe37cb26aec810ce708d56dbc643cc63c44f
        public EditorPage(Note note, User user)
        {
            this.note = note;
            this.user = user;

<<<<<<< HEAD
            Button traduce = new Button();
            traduce.BackgroundColor = Color.Transparent;
            traduce.HeightRequest = 50;
            traduce.WidthRequest = 70;
            traduce.Text = "Traduire";
            traduce.Clicked += OnTraduceCliked;
=======
            Button traduce = new Button
            {
                Text = "traduire",
                HeightRequest = 50,
                WidthRequest = 70
            };

            traduce.Clicked += OnTraduceCliked; 

>>>>>>> 2a99fe37cb26aec810ce708d56dbc643cc63c44f

            editor = new SfRichTextEditor();
            editor.AutoSize = AutoSizeOption.TextChanges;
            editor.VerticalOptions = LayoutOptions.CenterAndExpand;
            editor.HtmlText = note.Content;
            editor.HeightRequest = 1000;
            editor.PlaceHolder = "Votre note";
<<<<<<< HEAD
=======
            editor.TextChanged += OnTextChanged;

>>>>>>> 2a99fe37cb26aec810ce708d56dbc643cc63c44f
            if (editor.ToolbarItems.Count == 1)
                editor.ToolbarItems.Add(traduce);
            else
                editor.ToolbarItems[1] = traduce;

<<<<<<< HEAD
            editor.TextChanged += OnTextChanged;

            StackLayout stack = new StackLayout();
            stack.VerticalOptions = LayoutOptions.Start;
            stack.Children.Add(editor);
            this.Title = Directory.GetDirectoryById(note.IdDirectory).Title+"/"+note.Title;
=======
            StackLayout stack = new StackLayout();
            stack.VerticalOptions = LayoutOptions.Start;
            stack.Children.Add(editor);
            this.Title = Directory.GetDirectoryByIdAndIdOwner(note.IdDirectory, user).Title + "/" + note.Title;
>>>>>>> 2a99fe37cb26aec810ce708d56dbc643cc63c44f
            this.Content = stack;
        }

        public void OnTextChanged(object sender, Syncfusion.XForms.RichTextEditor.TextChangedEventArgs e)
        {
            note.Update(editor.HtmlText, user);
            string text = e.Text;
        }

        public void OnTraduceCliked(object sender, EventArgs e)
        {
<<<<<<< HEAD
            TranslationServiceClient client = TranslationServiceClient.Create();
            TranslateTextRequest request = new TranslateTextRequest
            {
                Contents = { this.editor.Text },
                TargetLanguageCode = "fr-FR",
                Parent = new ProjectName(projectID).ToString()
            };
            TranslateTextResponse response = client.TranslateText(request);
            Translation translation = response.Translations[0];
            string translateText = translation.TranslatedText;
            this.editor.Text = translateText; 
=======
            string text = editor.HtmlText;
            TranslationClient client = TranslationClient.Create();
            TranslationResult result = client.TranslateText(text, LanguageCodes.French);
            editor.HtmlText = result.TranslatedText;
>>>>>>> 2a99fe37cb26aec810ce708d56dbc643cc63c44f
        }
    }
}