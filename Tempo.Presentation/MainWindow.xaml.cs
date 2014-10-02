using System.Windows;
using System.Windows.Forms;

using GalaSoft.MvvmLight.Ioc;

namespace Tempo.Presentation
{
    //NOTE: I didn't succeed to use commands. Insted I had to use "regular events". That made mess all over the place
    public partial class MainWindow 
    { 
        private void ImportButton_OnClick(object sender, RoutedEventArgs e)
        {
            var songsToImport = _viewModel.songImporter.GetAll_fromDirectory();
            _viewModel.playlist.Add(songsToImport);
            
            // Update UI (TODO: Fix, this is an ugly approach)
            _viewModel.SongsList.Clear();
            foreach (var song in songsToImport)
            {
                _viewModel.SongsList.Add(song);
            }
        }

        private void PlayButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.audioPlayer.Play(_viewModel.SelectedSong);
        }
    }
    public partial class MainWindow : Window
    {
        private readonly ViewModel.MainWindowViewModel _viewModel = ViewModel.ViewModelLocator.Main;
        
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
