using System.Collections.Generic;
using System.Windows;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace Tempo.Presentation
{
    public partial class SettingsWindow : MetroWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();
            var appStyle = ThemeManager.DetectAppStyle(Application.Current);

            var allThemes = new List<string>()
            {
                "Teal",
                "Violet",
                "Red",
                "Green",
                "Blue"
            };
            this.ThemePicker.ItemsSource = allThemes;
            this.ThemePicker.SelectedItem = allThemes.Find(_ => _ == appStyle.Item2.Name);
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            var appStyle = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(
                Application.Current,
                ThemeManager.GetAccent(this.ThemePicker.SelectedItem as string),
                appStyle.Item1);
            this.Close();
        }

        private void DiscardChanges(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
