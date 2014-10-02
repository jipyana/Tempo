using System.Collections.ObjectModel;
using System.Windows;

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
            this.SongsList = new ObservableCollection<ent::Song>();
        }
    }
}
