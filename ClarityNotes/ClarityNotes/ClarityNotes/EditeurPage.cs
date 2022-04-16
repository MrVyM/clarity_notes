using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class EditeurPage : ContentPage
    {
        string PATH;
        Editor editor;
        public EditeurPage(string path)
        {

            PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "/data" + "/" + path + ".txt";
            StreamReader sr = new StreamReader(PATH);
            StackLayout mainContent = new StackLayout();
            editor = new Editor();
            editor.HeightRequest = App.Current.MainPage.Height;
            editor.IsTextPredictionEnabled = false;
            editor.Text = sr.ReadToEnd();
            sr.Close();
            editor.TextChanged += OnSaveClicked;

            mainContent.Children.Add(editor);
            this.Title = "  --- "+path;
            this.Content = mainContent;
        }
        private void OnSaveClicked(Object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(PATH);
            sw.Write(editor.Text);
            sw.Close();
        }
    }
}