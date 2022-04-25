using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class EditorPage : ContentPage
    {
        Note note;
        User user;
        Editor editor;

        public EditorPage(Note note, User user)
        {
            this.note = note;
            this.user = user;
            StackLayout mainContent = new StackLayout();
            this.Title = note.Title;
            this.Content = mainContent;
        }

        private void OnSaveClicked(Object sender, EventArgs e)
        {
            note.Update(editor.Text, user);
        }
    }
}