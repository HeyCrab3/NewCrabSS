using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace NewCrabSS.ThemeHandler
{
    internal class CustomizeSettings
    {
        public static int ChangeTheme(string theme)
        {
            var paletteHelper = new PaletteHelper();
            ITheme themes = paletteHelper.GetTheme();
            if (theme == "light")
            {
                themes.SetBaseTheme(Theme.Light);
                themes.SetPrimaryColor(Colors.Teal);
                themes.SetSecondaryColor(Colors.LightGreen);
                paletteHelper.SetTheme(themes);
                return 1;
            }else if (theme == "dark")
            {
                themes.SetBaseTheme(Theme.Dark);
                themes.SetPrimaryColor(Colors.Teal);
                themes.SetSecondaryColor(Colors.LightGreen);
                paletteHelper.SetTheme(themes);
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
