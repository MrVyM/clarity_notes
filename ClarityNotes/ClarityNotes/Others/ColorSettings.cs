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
            // BackGround - Button - 
            Color[] colors = new Color[3];
            switch (colorSettings)
            {
                case ColorSettings.Blue:
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
    }
}