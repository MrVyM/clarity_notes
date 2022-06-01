using System;
using Xamarin.Forms;

namespace ClarityNotes
{
    public class ThemeChangePage : ContentPage
    {
        private User user;
        private Picker picker;

        public ThemeChangePage(User user)
        {
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

            foreach (string colorName in new string[] { "Blue", "Green", "Red", "Yellow", "Pink", "Orange" })
                picker.Items.Add(colorName);

            Button submit = new Button()
            {
                Text = "Confirmation",
                HorizontalOptions = LayoutOptions.Center
            };
            submit.Clicked += OnSubmitClicked;

            stackLayout.Children.Add(questionLabel);
            stackLayout.Children.Add(picker);

            mainContent.Children.Add(new Frame() { Margin = 25, Content = stackLayout });
            mainContent.Children.Add(submit);
            this.Content = mainContent;
            this.BackgroundColor = user.ColorTheme;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (picker.SelectedItem == null) return;
            switch (picker.SelectedItem.ToString())
            {
                case "Blue":
                    user.UpdateColorTheme(Color.Blue);
                    break;
                case "Green":
                    user.UpdateColorTheme(Color.Green);
                    break;
                case "Red":
                    user.UpdateColorTheme(Color.Red);
                    break;
                case "Yellow":
                    user.UpdateColorTheme(Color.Yellow);
                    break;
                case "Pink":
                    user.UpdateColorTheme(Color.Pink);
                    break;
                case "Orange":
                    user.UpdateColorTheme(Color.Orange);
                    break;
            }
            var page = new RootPage(user);
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushAsync(page);
            Navigation.RemovePage(this);
        }
    }
}