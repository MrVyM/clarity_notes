using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class WaitingPage : ContentPage
    {
        public WaitingPage()
        {

            StackLayout mainContent = new StackLayout();

            var webImage = new Image
            {
                Source = ImageSource.FromUri(
                    new Uri("http://2.7.138.151/assets/images/logo.png")
                )
            };
            
            mainContent.Children.Add(webImage); 
            this.Content = mainContent;
            System.Threading.Thread.Sleep(2000);
            Navigation.PushAsync(new LoginPage());
            Navigation.RemovePage(this);

        }
    }
}