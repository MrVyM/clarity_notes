using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClarityNotes
{
    public class RootPage : ContentPage
    {
        public RootPage()
        {

            Label testLabel = new Label();

            testLabel.Text = "test";

            StackLayout mainContent = new StackLayout();
            mainContent.Orientation = StackOrientation.Horizontal;
            mainContent.VerticalOptions = LayoutOptions.Start;
            mainContent.HorizontalOptions = LayoutOptions.Start;


            Frame frameColumn = new Frame();
            frameColumn.VerticalOptions = LayoutOptions.StartAndExpand;
            frameColumn.HorizontalOptions = LayoutOptions.StartAndExpand;   


            StackLayout verticalColumn = new StackLayout();
            verticalColumn.HorizontalOptions = LayoutOptions.Center;
            verticalColumn.VerticalOptions = LayoutOptions.StartAndExpand;
            verticalColumn.Margin = 10;


            for (int i = 0; i < 10; i++)
            {
                Button but = new Button();
                but.Text = i.ToString();
                but.WidthRequest = 50;
                but.Clicked += OnButtonCliked;
                verticalColumn.Children.Add(but);
            }

            frameColumn.Content = verticalColumn;

            mainContent.Children.Add(frameColumn);
            mainContent.Children.Add(testLabel);
            this.Content = mainContent;
        }

        private void OnButtonCliked(object sender, EventArgs e)
        {
            Console.WriteLine(((Button)sender).Text);
        }
    }
}