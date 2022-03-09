using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class RootPage : ContentPage
    {
        string PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/data";

        public RootPage()
        {
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
                
            }
            Directory.CreateDirectory(PATH + "/tet");
            Console.WriteLine(PATH);

            StackLayout mainContent = new StackLayout();
            mainContent.Orientation = StackOrientation.Horizontal;
            mainContent.VerticalOptions = LayoutOptions.CenterAndExpand;
            mainContent.HorizontalOptions = LayoutOptions.Start;

            StackLayout verticalLayout = new StackLayout();

            Frame frameColumn = new Frame();
            frameColumn.VerticalOptions = LayoutOptions.StartAndExpand;
            frameColumn.HorizontalOptions = LayoutOptions.StartAndExpand;


            StackLayout verticalColumn = new StackLayout();
            verticalColumn.HorizontalOptions = LayoutOptions.Center;
            verticalColumn.VerticalOptions = LayoutOptions.CenterAndExpand;


            foreach (string dir in Directory.EnumerateDirectories(PATH))
            {
                Button button = new Button();
                button.Clicked += OnButtonClicked;
                button.Text = Path.GetFileName(dir);
                verticalColumn.Children.Add(button);
            }

            frameColumn.Content = verticalColumn;

            Frame AddFrame = new Frame();
            StackLayout AddLayout = new StackLayout();

            Button add = new Button() { Text = "+" };
            add.Clicked += OnAddClicked;
            AddLayout.Children.Add(add);

            Button remove = new Button() { Text = "-" };
            remove.Clicked += OnRemoveClicked;
            AddLayout.Children.Add(remove);

            Button settings = new Button() { Text = "SET" };
            AddLayout.Children.Add(settings);

            StackLayout listNotes = new StackLayout();
            listNotes.Margin = 15;

            foreach (var dir in Directory.EnumerateFiles(PATH))
            {
                Console.WriteLine(dir);
                Label temp = new Label();
                temp.Text = Path.GetFileName(dir);
                listNotes.Children.Add(temp);
            }

            verticalLayout.Children.Add(verticalColumn);
            verticalLayout.Children.Add(AddLayout);
            mainContent.Children.Add(verticalLayout);
            mainContent.Children.Add(listNotes);
            this.Content = mainContent;
            this.Content = mainContent;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine(((Button)sender).Text);
        }
        private void OnAddClicked(object sender, EventArgs e)
        {
            var page = new AddChapterPage();
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }

        private void OnRemoveClicked(object sender, EventArgs e)
        {
            var page = new RemoveChapterPage();
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
        }
    }
}