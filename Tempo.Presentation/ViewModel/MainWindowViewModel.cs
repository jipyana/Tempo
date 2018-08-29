using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Tempo.Main.Model;
using Tempo.Main.Model.Impl;
using Tempo.Services.AudioPlayer;
using ent = Tempo.Main.Entities;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel : DependencyObject
    {
        //MainWindow n = new MainWindow();

        public int ProgressBarMax
        {
            get { return (int)GetValue(ProgressBarMaxProperty); }
            set { SetValue(ProgressBarMaxProperty, value); }
        }
        public static readonly DependencyProperty ProgressBarMaxProperty = DependencyProperty.Register("ProgressBarMaximum", typeof(Slider), typeof(MainWindowViewModel), new PropertyMetadata());

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

        public ent::Song PlayingSong
        {
            get { return (ent::Song) GetValue(PlayingSongProperty); }
            set { SetValue(PlayingSongProperty, value); }
        }
        public static readonly DependencyProperty PlayingSongProperty = DependencyProperty.Register("PlayingSong", typeof (ent::Song), typeof (MainWindowViewModel), new PropertyMetadata());

        public string PlaybackLoopText
        {
            get { return (string)GetValue(PlaybackLoopTextProperty); }
            set { SetValue(PlaybackLoopTextProperty, value); }
        }
        public static readonly DependencyProperty PlaybackLoopTextProperty = DependencyProperty.Register("PlaybackLoopText", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata());

        //public List<string> GetAllSongNames
        //{
        //    get
        //    {
        //        List<string> songNames = new List<string>();
        //        foreach (var item in n.GetAllSongsFromCloudLibrary())
        //        {
        //            songNames.Add(item.title);
        //        }
        //        return songNames;
        //    }
        //}
        public MainWindowViewModel()
        {
            
            songImporter = new SongsImporter();
            playlist     = new XmlPlaylist();
            audioPlayer  = new AudioPlayer();

            this.SongsList = new ObservableCollection<ent::Song>(playlist.GetAll());
            this.TogglePlaybackLoopCommand.Execute(null);
            audioPlayer.OnPlaybackEnded += () =>
            {
                var currentSongIndex = SongsList.IndexOf(PlayingSong);
                var currentSongIsLastOne = currentSongIndex == SongsList.Count - 1;
                if (!currentSongIsLastOne)
                {
                    var nextSong = SongsList[currentSongIndex + 1];
                    audioPlayer.Play(nextSong);
                    this.PlayingSong = nextSong;
                }
                else if (IsPlaybackLoopOn)
                {
                    var nextSong = SongsList[0];
                    audioPlayer.Play(nextSong);
                    this.PlayingSong = nextSong;
                }
                MainWindow.GetAllSongsFromCloudLibrary();
            };
        }
        private readonly ISongsImporter songImporter;
        private readonly IPlaylist      playlist;
        private readonly IAudioPlayer   audioPlayer;

        private bool IsPlaybackLoopOn { get; set; }
    }
}
