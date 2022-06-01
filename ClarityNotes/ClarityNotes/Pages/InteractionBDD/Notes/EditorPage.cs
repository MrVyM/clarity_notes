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
        
        public EditorPage(Note note, User user)
        {
            this.note = note;
            this.user = user;
            StackLayout stack = new StackLayout();
            SfRichTextEditor editor = new SfRichTextEditor
            {
                AutoSize = AutoSizeOption.TextChanges,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = ""
            };
            stack.Children.Add(editor);
            this.Content = stack;
        }
    }
}