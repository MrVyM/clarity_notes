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

        public EditorPage(Note note, User user)
        {
            this.note = note;
            this.user = user;

            Button traduce = new Button
            {
                Text = "traduire",
                HeightRequest = 50,
                WidthRequest = 70
            };

            traduce.Clicked += OnTraduceClicked;

            Button QRCode = new Button
            {
                Text = "Obtenir un QRCode",

                HeightRequest = 50,
                WidthRequest = 100,
            };

            QRCode.Clicked += OnQRCodeGeneratorClicked;

            editor = new SfRichTextEditor();
            editor.AutoSize = AutoSizeOption.TextChanges;
            editor.VerticalOptions = LayoutOptions.CenterAndExpand;
            editor.HtmlText = note.Content;
            editor.HeightRequest = 1000;
            editor.PlaceHolder = "Votre note";

            if (editor.ToolbarItems.Count == 1)
                editor.ToolbarItems.Add(traduce);
            else
                editor.ToolbarItems[1] = traduce;

            if (editor.ToolbarItems.Count == 1)
                editor.ToolbarItems.Add(QRCode);
            else
                editor.ToolbarItems[1] = QRCode;

            editor.TextChanged += OnTextChanged;

            StackLayout stack = new StackLayout();
            stack.VerticalOptions = LayoutOptions.Start;
            stack.Children.Add(editor);
            this.Title = Directory.GetDirectoryByIdAndIdOwner(note.IdDirectory, user).Title + "/" + note.Title;
            this.Content = stack;
        }

        public void OnTextChanged(object sender, Syncfusion.XForms.RichTextEditor.TextChangedEventArgs e)
        {
            note.Update(editor.HtmlText, user);
            string text = e.Text;
        }

        public void OnQRCodeGeneratorClicked(object sender, EventArgs e)
        {
            var QRpage = new QRCodeGeneratorPage(user, note);
            NavigationPage.SetHasNavigationBar(QRpage, false);
            Navigation.PushAsync(QRpage);
        }
        public void OnTraduceClicked(object sender, EventArgs e)
        {
            string route = "/translate?api-version=3.0&to=de&to=it&to=ja&to=th";
            string text = editor.Text;
            var rep = Traductor.Traduce(Traductor.SubscriptionKey, Traductor.Endpoint, route, text);
            rep.Wait();
            editor.HtmlText = rep.Result;
        }
    }
}       
            
            
       
 