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
        public SfRichTextEditor editorWindows;
        public Editor editorAndroid;

        public EditorPage(Note note, User user)
        {
            this.note = note;
            this.user = user;


            if (Device.RuntimePlatform == Device.UWP)
            {
                Button traduce = new Button
                {
                    Text = "Traduire",
                    HeightRequest = 50,
                    WidthRequest = 70
                };

                traduce.Clicked += OnTraduceClicked;

                editorWindows = new SfRichTextEditor();
                editorWindows.AutoSize = AutoSizeOption.TextChanges;
                editorWindows.VerticalOptions = LayoutOptions.CenterAndExpand;
                editorWindows.HtmlText = note.Content;
                editorWindows.HeightRequest = 1000;
                editorWindows.ReadOnly = Directory.GetReadOnlyByNoteAndIdOwner(note, user);
                editorWindows.PlaceHolder = $"Bienvenue sur la note [{note.Title}], vous pouvez insérer votre propre texte.";

                if (!Directory.GetReadOnlyByNoteAndIdOwner(note, user))
                {
                    if (editorWindows.ToolbarItems.Count == 1)
                    {
                        editorWindows.ToolbarItems.Add(traduce);
                    }
                    else
                    {
                        editorWindows.ToolbarItems[1] = traduce;
                    }
                }

                editorWindows.TextChanged += OnTextChangedWindows;

                StackLayout stack = new StackLayout();
                stack.VerticalOptions = LayoutOptions.Start;
                stack.Children.Add(editorWindows);
                this.Content = stack;
            }
            else 
            {
                editorAndroid = new Editor();
                editorAndroid.Text = note.Content;
                editorAndroid.IsReadOnly = Directory.GetReadOnlyByNoteAndIdOwner(note, user);
                editorAndroid.TextChanged += OnTextChangedAndroid;
                editorAndroid.AutoSize = EditorAutoSizeOption.TextChanges;

                StackLayout stack = new StackLayout();
                stack.VerticalOptions = LayoutOptions.Start;
                stack.Children.Add(editorAndroid);
                this.Content = stack;

            }
            this.Title = Directory.GetDirectoryByIdAndIdOwner(note.IdDirectory, user).Title + "/" + note.Title;
            
        }

        public void OnTextChangedWindows(object sender, Syncfusion.XForms.RichTextEditor.TextChangedEventArgs e)
        {
            note.Update(editorWindows.HtmlText, user);
            string text = editorWindows.Text;
            note.Update(text, user);
        }

        public void OnTextChangedAndroid(object sender,EventArgs e)
        {
            note.Update(editorAndroid.Text, user);
            string text = editorAndroid.Text;
            note.Update(text,user);
        }

        public void OnTraduceClicked(object sender, EventArgs e)
        {
            string text;
            if (editorAndroid == null)
                text = editorWindows.HtmlText;
            else
                text = editorAndroid.Text;
            note.Update(text, user);
            var page = new LanguageTraductorPage(this, user, note.Id);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
    }
}       
            
            
       
 