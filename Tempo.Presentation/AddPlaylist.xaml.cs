using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tempo.Presentation
{
    /// <summary>
    /// Interaction logic for AddPlaylist.xaml
    /// </summary>
    public partial class AddPlaylist : Window
    {

        private ObservableCollection<Tuple<String, int>> GetSongNames;

        //public IEnumerable<Song> GetSongs()
        //{
        //}
        //private readonly ViewModel.MainWindowViewModel vm;
        public AddPlaylist()
        {
            InitializeComponent();
            GetSongNames = new ObservableCollection<Tuple<string, int>>();
            List<Tempo.CloudModels.Song> songs = MainWindow.GetAllSongsFromCloudLibrary();
            foreach (Tempo.CloudModels.Song s in songs)
            {
                Tuple<String, int> song = new Tuple<string, int>(s.title, s.id);
                GetSongNames.Add(song);
            }
            foreach (var item in GetSongNames)
            {
                liveMusic.Items.Add(item);
            }


            


            // vm = new ViewModel.MainWindowViewModel();
            //this.DataContext = vm;
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            
            var musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            Directory.CreateDirectory(musicFolder + "/" + folderName.Text + "/");
            string path = "";
            path = musicFolder + "/" + folderName.Text + "/";

            foreach (Tuple<String, int> item in newPlaylist.Items)
            {
                try
                {
                    string json = "";
                    int id = item.Item2;
                    string httpRequestString = $"http://kaden.ghostsofutah.com:9578/music/getfile/{id}";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpRequestString);
                    request.AutomaticDecompression = DecompressionMethods.GZip;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }

                    Tempo.CloudModels.SongWithFileBytes songWithBytes = Newtonsoft.Json.JsonConvert.DeserializeObject<Tempo.CloudModels.SongWithFileBytes>(json);
                    //path += get to users music folder
                    //create tempo folder
                    System.IO.File.WriteAllBytes(path + songWithBytes.song.title + ".mp3", songWithBytes.fileBytes);
                }
                catch (Exception e3)
                {
                    MessageBox.Show("Error occured trying to download your song");
                }
            }

        }

        private void AddSong_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in new ArrayList(liveMusic.SelectedItems))
            {
                newPlaylist.Items.Add(item);
            }
        }

        private void RemoveSong_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in new ArrayList(newPlaylist.SelectedItems))
            {
                newPlaylist.Items.Remove(item);
            }
        }
    }
}
