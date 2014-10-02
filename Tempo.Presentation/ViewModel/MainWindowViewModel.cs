using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Tempo.Main.Model;
using Tempo.Main.Model.Impl;
using ent = Tempo.Main.Entities;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel : DependencyObject
    {
        public ObservableCollection<ent::Song> SongsList
        {
            get { return (ObservableCollection<ent::Song>)GetValue(ImagesListProperty); }
            set { SetValue(ImagesListProperty, value); }
        }
        public static readonly DependencyProperty ImagesListProperty = DependencyProperty.Register("SongsList", typeof(ObservableCollection<ent::Song>), typeof(MainWindowViewModel), new UIPropertyMetadata());

        public ent::Song SelectedSong
        {
            get { return (ent::Song) GetValue(SelectedSongProperty); }
            set { SetValue(SelectedSongProperty, value); }
        }
        public static readonly DependencyProperty SelectedSongProperty = DependencyProperty.Register("SelectedSong", typeof (ent::Song), typeof (MainWindowViewModel), new PropertyMetadata());


        public MainWindowViewModel
        (
            IPlaylist      playlist,
            ISongsImporter songsImporter
        )
        {
            this.SongsList = new ObservableCollection<ent::Song>();

            this.playlist     = playlist;
            this.songImporter = songsImporter;
        }
        private readonly ISongsImporter songImporter;
        private readonly IPlaylist      playlist;
    }
}
