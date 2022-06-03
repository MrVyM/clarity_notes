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
        SfRichTextEditor editorWindows;
        Editor editorAndroid;

        public EditorPage(Note note, User user)
        {
            this.note = note;
            this.user = user;


            if (Device.RuntimePlatform == Device.UWP)
            {
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

                editorWindows = new SfRichTextEditor();
                editorWindows.AutoSize = AutoSizeOption.TextChanges;
                editorWindows.VerticalOptions = LayoutOptions.CenterAndExpand;
                editorWindows.HtmlText = note.Content;
                editorWindows.HeightRequest = 1000;
                editorWindows.PlaceHolder = "Votre note";

                if (editorWindows.ToolbarItems.Count == 1)
                    editorWindows.ToolbarItems.Add(traduce);
                else
                    editorWindows.ToolbarItems[1] = traduce;

                if (editorWindows.ToolbarItems.Count == 1)
                    editorWindows.ToolbarItems.Add(QRCode);
                else
                    editorWindows.ToolbarItems[1] = QRCode;

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

        public void OnQRCodeGeneratorClicked(object sender, EventArgs e)
        {
            var QRpage = new QRCodeGeneratorPage(user, note);
            NavigationPage.SetHasNavigationBar(QRpage, false);
            Navigation.PushAsync(QRpage);
        }
        public void OnTraduceClicked(object sender, EventArgs e)
        {
            string route = "/translate?api-version=3.0&to=de&to=it&to=ja&to=th";
            string text;
            if (editorAndroid == null)
                text = editorWindows.Text;
            else
                text = editorAndroid.Text;
            var rep = Traductor.Traduce(Traductor.SubscriptionKey, Traductor.Endpoint, route, text);
            rep.Wait();
            if (editorAndroid == null)
                editorWindows.HtmlText = rep.Result;
            else
                editorAndroid.Text = rep.Result;
        }
    }
}       
            
            
       
 