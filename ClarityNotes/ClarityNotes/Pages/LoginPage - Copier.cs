using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class WebPage : ContentPage
    {
        public WebPage(string source)
        {
            StackLayout stackLayout = new StackLayout();
            var webView = new WebView { HeightRequest = 500, Source = source };
            stackLayout.Children.Add(webView);
            Content = stackLayout;
        }

    }
}