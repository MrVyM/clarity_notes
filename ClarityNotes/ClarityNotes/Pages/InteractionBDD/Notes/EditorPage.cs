using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.XForms.RichTextEditor;


namespace ClarityNotes
{
    public class EditorPage : ContentPage
    {
        Note note;
        User user;
        SfRichTextEditor editor;


        public EditorPage(Note note, User user)
        {
            this.note = note;
            this.user = user;
            StackLayout stack = new StackLayout();
<<<<<<< HEAD
            SfRichTextEditor editor = new SfRichTextEditor
            {
                AutoSize = AutoSizeOption.TextChanges,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = ""
            };
=======
            stack.VerticalOptions = LayoutOptions.Start;
            editor = new SfRichTextEditor();
            editor.AutoSize = AutoSizeOption.TextChanges;
            editor.HeightRequest = 1000;
            editor.HtmlText = note.Content;
            editor.PlaceHolder = "Votre note";
            editor.TextChanged += OnTextChanged;
>>>>>>> refs/remotes/origin/main
            stack.Children.Add(editor);
            this.Content = stack;
        }

        public void OnTextChanged(object sender, EventArgs e)
        {
            note.Update(editor.HtmlText, user);
        }
    }
}