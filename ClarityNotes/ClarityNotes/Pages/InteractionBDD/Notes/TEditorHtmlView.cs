using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using TEditor;
using TEditor.Abstractions;

namespace ClarityNotes
{
    public class TEditorHtmlView : StackLayout
    {
        public string Html { get; set; }
        public WebView displayWebView;


        public TEditorHtmlView(string name)
        {
            this.Orientation = StackOrientation.Vertical;
            this.Children.Add(new Button
            {
                Text = "Editer la note suivante : " + name,
                HeightRequest = 100,
                Command = new Command(async (obj) =>
                {
                    await ShowTEditor();
                })
            }); 
            displayWebView = new WebView() { HeightRequest = 500 };
            this.Children.Add(displayWebView);
        }

        async Task ShowTEditor()
        {
            TEditorResponse response = await CrossTEditor.Current.ShowTEditor("<p>XAM consulting</p>");
            if (!string.IsNullOrEmpty(response.HTML))
                displayWebView.Source = new HtmlWebViewSource() { Html = response.HTML };
        }
    }
}