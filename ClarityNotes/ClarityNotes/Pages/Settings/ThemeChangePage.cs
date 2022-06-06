using System;
using System.Collections.Generic;
using Xamarin.Forms;
namespace ClarityNotes
{
    public class ThemeChangePage : ContentPage
    {
        private User user;
        private Picker picker;

        private Dictionary<string, string> Theme = new Dictionary<string, string>();
        public ThemeChangePage(User user)
        {
            Theme.Add("Pink", "FFC0CB");
            Theme.Add("Blue", "57B1EB");
            Theme.Add("Green", "66A37B");
            Theme.Add("Red", "E87C6B");
            Theme.Add("Yellow", "D1C860");
            Theme.Add("Orange", "E77E22");
            Theme.Add("Purple", "A87CA0");
            Theme.Add("Cyan", "20B2AA");
            Theme.Add("Brown", "A0522D");
            Theme.Add("White", "d6d6d6");
            Theme.Add("Gray", "363636");
            Theme.Add("Gold", "f7d705");


            this.user = user;

            StackLayout mainContent = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 500
            };

            StackLayout stackLayout = new StackLayout();

            Label questionLabel = new Label()
            {
                FontSize = 16,
                Text = "Quel thème souhaitez-vous utiliser ?",
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center
            };

            picker = new Picker
            {
                Title = "Sélection du thème",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string colorName in Theme.Keys)
                picker.Items.Add(colorName);

            Button submit = new Button()
            {
                Text = "Confirmation",
                HorizontalOptions = LayoutOptions.Center
            };
            submit.Clicked += OnSubmitClicked;

            stackLayout.Children.Add(questionLabel);
            stackLayout.Children.Add(picker);

            mainContent.Children.Add(new Frame() { Margin = 25, BackgroundColor = Color.White, Content = stackLayout });
            mainContent.Children.Add(submit);
            this.Content = mainContent;
            this.BackgroundColor = user.ColorTheme;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (picker.SelectedItem == null) return;
            user.UpdateColorTheme(Color.FromHex(Theme[picker.SelectedItem.ToString()]));
            var page = new RootPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
        }
    }
}