using Xamarin.Forms;

namespace ClarityNotes
{
    public enum ColorSettings
    {
        Blue,
        Green,
        Red,
        Yellow,
        Pink,
        Orange
    }

    public static class ColorSettingsUsers
    {
        public static Color[] GetColors(ColorSettings colorSettings)
        {
            // BackgroundPage - BackgroundFrame - BackgroundButton
            Color[] colors = null;
            switch (colorSettings)
            {
                case ColorSettings.Blue:
                    colors = new Color[] { Color.FromHex("33B0FF"), Color.White, Color.FromHex("298dcc") };
                    break;
                case ColorSettings.Green:
                    break;
                case ColorSettings.Red:
                    break;
                case ColorSettings.Yellow:
                    break;
                case ColorSettings.Pink:
                    break;
                case ColorSettings.Orange:
                    break;
            }
            return colors;
        }

        public static string[] GetColorsName()
        {
            return new string[] { "Blue", "Green", "Red", "Yellow", "Pink", "Orange" };
        }
    }
}