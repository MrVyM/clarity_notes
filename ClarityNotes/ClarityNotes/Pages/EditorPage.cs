using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using TEditor;
using TEditor.Abstractions;

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
            Content = new TEditorHtmlView(this.note.Title);
            BackgroundColor = Color.FromHex("57b1eb");
        }

    }
}