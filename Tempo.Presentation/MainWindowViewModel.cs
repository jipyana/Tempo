using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using Tempo.Infrastructure.AudioPlayer;
using Tempo.Main.Model;
using Tempo.Main.Model.Impl;
using ent = Tempo.Main.Entities;

namespace Tempo.Presentation
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


        public MainWindowViewModel()
        {
            songImporter = new SongsImporter();
            playlist     = new XmlPlaylist();
            audioPlayer  = new AudioPlayer();

            this.SongsList = new ObservableCollection<ent::Song>(playlist.GetAll());
        }
        private readonly ISongsImporter songImporter;
        private readonly IPlaylist      playlist;
        private readonly IAudioPlayer   audioPlayer;
    }
}
