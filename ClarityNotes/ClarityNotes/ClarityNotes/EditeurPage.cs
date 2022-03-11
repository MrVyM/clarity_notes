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

            PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/data" + "/" + path + ".txt";
            StreamReader sr = new StreamReader(PATH);
            StackLayout mainContent = new StackLayout();
            editor = new Editor();
            editor.HeightRequest = 200;
            editor.Text = sr.ReadToEnd();
            sr.Close();

            Button save = new Button();
            save.Clicked += OnSaveClicked;
            mainContent.Children.Add(editor);
            this.Title = path;
            this.Content = mainContent;
        }
        private void OnSaveClicked(Object sender, EventArgs e)
        {
            Console.WriteLine("Faut encore Save");
        }
    }
}