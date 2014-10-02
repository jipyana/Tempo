using System.Windows;

using Tempo.Main.Model.Impl;

namespace Tempo.Presentation
{
    public partial class MainWindow
    {
        private void ImportButton_OnClick(object sender, RoutedEventArgs e)
        {
            var songImporter = new SongsImporter();

            songImporter.GetAll_fromDirectory();
        }
    }
}
