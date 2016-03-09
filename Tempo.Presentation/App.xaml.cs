using System.Windows;
using MahApps.Metro;

namespace Tempo.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ThemeManager.ChangeAppStyle(
                Application.Current,
                ThemeManager.GetAccent("Teal"),
                ThemeManager.GetAppTheme("BaseLight"));
        }
    }
}
