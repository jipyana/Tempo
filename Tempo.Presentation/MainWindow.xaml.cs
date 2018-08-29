using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Tempo.CloudModels;
using Tempo.Presentation.UserControls;
using Tempo.Presentation.ViewModel;
using System.Windows.Documents;
using System.Net;
using System.IO;
using Microsoft.Win32;
using System.Net.Http;
using System.ComponentModel;
using System.Windows.Data;

namespace Tempo.Presentation
{
    public partial class MainWindow : MetroWindow
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        private readonly ViewModel.MainWindowViewModel vm;
        public ObservableCollection<Tempo.CloudModels.ListViewSong> CloudSongList;
        public static DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {

            InitializeComponent();
            vm = new ViewModel.MainWindowViewModel();
            this.DataContext = vm;
            sliProgress.Minimum = 0;
            sliProgress.Value = 0;

            CloudSongList = new ObservableCollection<ListViewSong>();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            //timer.Start();
            SetSongsToTable(GetAllSongsFromCloudLibrary());


            var musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

            var directories = Directory.GetDirectories(musicFolder);

            foreach (var item in directories)
            {


                string[] directoryArr = item.ToString().Split('\\');

                myPlaylist.Items.Add(directoryArr[directoryArr.Length - 1]);

                this.Show();
            }
               
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            sliProgress.Value++;
            int hours = (int)(sliProgress.Value / 3600);
            int mins = (int)(sliProgress.Value - hours * 3600) / 60;
            int secs = (int)(sliProgress.Value - (hours * 3600) - (mins * 60));
            lblProgressStatus.Text = "" + hours + ":" + mins + ":" + secs;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        //private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        //{
        //    userIsDraggingSlider = false;
        //    mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        //}

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        //private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    GridMain.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        //}

        private void PlaylistElement_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.PlayCommand.Execute(null);
            //play.IsEnabled = true;


        }

        private void OpenSettingsWindow(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            var songs = mainWindowViewModel.SongsList;
            window.ShowDialog();
        }


        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (cloudListView.SelectedItems.Count > 1)
            {
                foreach (ListViewSong s in cloudListView.SelectedItems)
                {
                    DownloadSongFromIdAsync(Int32.Parse(s.Id));
                }
            }
            else
            {
                ListViewSong s = (ListViewSong)cloudListView.SelectedItem;
                DownloadSongFromIdAsync(Int32.Parse(s.Id));
            }
        }

        private async void DownloadSongFromIdAsync(int id)
        {
            try
            {
                string json = "";
                string httpRequestString = $"http://kaden.ghostsofutah.com:9578/music/getfile/{id}";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpRequestString);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }

                SongWithFileBytes songWithBytes = Newtonsoft.Json.JsonConvert.DeserializeObject<SongWithFileBytes>(json);
                string path = "";
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "/Tempo/Downloads/";
                (new FileInfo(path)).Directory.Create();
                //path += get to users music folder
                //create tempo folder
                path += songWithBytes.song.title + ".mp3";
                System.IO.File.WriteAllBytes(path, songWithBytes.fileBytes);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occured trying to download your song");
            }
        }

        private void ClearCloudTable()
        {
            while (CloudSongList.Count > 0)
            {
                CloudSongList.RemoveAt(0);
            }
            while (cloudListView.Items.Count > 0)
            {
                cloudListView.Items.RemoveAt(0);
            }
        }

        // kaden.ghostsofutah.com:9578/music/help
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            String title = titleTextBox.Text;
            String artist = artistTextBox.Text;
            String genre = genreTextBox.Text;

            string json = string.Empty;
            string httpRequestString = $"http://kaden.ghostsofutah.com:9578/music/getSongs/title={title}&artist={artist}&genre={genre}";
            // $"http://kaden.ghostsofutah.com:9578/music/getSongs/title={title}&artist={artist}&genre={genre}";

            Console.WriteLine(httpRequestString);
            //HTTP request with search parameters
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpRequestString);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            //json = json.Replace("\"", "");

            // Convert JSON into ArrayList<Song>        
            List<Song> songs = ConvertSongsFromJSON(json);

            SetSongsToTable(songs);

            // Put all of them into table


        }

        public void SetSongsToTable(List<Song> songs)
        {
            ClearCloudTable();
            foreach (Song s in songs)
            {
                string fileSize = "" + s.fileSize / 1000 + "mb";
                string id = "" + s.id;
                var item = new ListViewSong();
                item.Title = s.title;
                item.Artist = s.artist;
                item.Genre = s.genre;
                item.FileSize = fileSize;
                item.Id = id;
                CloudSongList.Add(item);

                //cloudLibraryTable.RowGroups[0].Rows.Add(tableRow);
            }

            foreach (ListViewSong lvs in CloudSongList)
            {
                cloudListView.Items.Add(lvs);
            }
            //ICollectionView view = CollectionViewSource.GetDefaultView(CloudSongList);
            //view.Refresh();
        }

        public static List<string> GetSongNames()
        {
            List<string> songNames = new List<string>();
            foreach (var item in GetAllSongsFromCloudLibrary())
            {
                songNames.Add(item.title);
            }
            return songNames;

        }


        public static List<Song> GetAllSongsFromCloudLibrary()
        {
            string json = string.Empty;
            string httpRequestString = "http://kaden.ghostsofutah.com:9578/music/getAllSongs";
            // "http://kaden.ghostsofutah.com:9578/music/getAllSongs";

            Console.WriteLine(httpRequestString);
            //HTTP request with search parameters
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpRequestString);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            //json = json.Replace("\"", "");

            // Convert JSON into ArrayList<Song>        
            return ConvertSongsFromJSON(json);
        }

        private static List<Song> ConvertSongsFromJSON(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(json);
        }

        public void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            cloudLibraryDocReader.Visibility = Visibility.Collapsed;
            UploadFormGrid.Visibility = Visibility.Visible;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            filePathUploadTextBox.Text = "";
            titleUploadTextBox.Text = "";
            artistUploadTextBox.Text = "";
            genreUploadTextBox.Text = "";

            cloudLibraryDocReader.Visibility = Visibility.Visible;
            UploadFormGrid.Visibility = Visibility.Collapsed;
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //cloudLibraryTable.Focus = Visibility.Hidden;
            //uploadGrid.Focus = Visibility.Visible;
            //this ^^^ includes all textbox fields and a submit and cancel button

            //First have user select mp3 file
            //Get the song detail strings from pop up window

            string filePath = filePathUploadTextBox.Text;
            if (filePath == "" || filePath == null || filePath.Split('.')[filePath.Split('.').Length - 1] != "mp3")
            {
                MessageBox.Show("That is an invalid File please choose an mp3 file before submitting");
            }
            else
            {
                string title = titleUploadTextBox.Text;
                string artist = artistUploadTextBox.Text;
                string genre = genreUploadTextBox.Text;
                int hours = 0;// Make some kinda time entry thing for the file
                int minutes = 0;
                int seconds = 0;

                if (File.Exists(filePath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    //get filesize from file
                    int fileSize = fileBytes.Length / 1000;
                    Song s = new Song();

                    s.title = title;
                    s.artist = artist;
                    s.genre = genre;
                    s.hours = hours;
                    s.minutes = minutes;
                    s.seconds = seconds;
                    s.fileSize = fileSize;
                    SongWithFileBytes songWithFile = new SongWithFileBytes(s, fileBytes);

                    string songWithFileJson = Newtonsoft.Json.JsonConvert.SerializeObject(songWithFile);

                    //convert songWithFile to json
                    //send songWithFile to:
                    //  http://kaden.ghostsofutah.com:9578/music/
                    //  with a post request and the json in the body

                    var httpClient = new HttpClient();
                    var response = await httpClient.PostAsync("http://kaden.ghostsofutah.com:9578/music/upload", new StringContent(songWithFileJson, System.Text.Encoding.UTF8, "application/json"));
                    // "http://kaden.ghostsofutah.com:9578/music/upload"
                    response.EnsureSuccessStatusCode();

                    CancelButton_Click(null, null);
                }
                else
                {
                    MessageBox.Show("The specified File does not exist");
                }

            }

        }

        private void addPlaylist_Click(object sender, RoutedEventArgs e)
        {
            AddPlaylist addPlaylist = new AddPlaylist();
            addPlaylist.Show();
        }
        private void mp3FileBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "MP3 files (*.mp3)|*.mp3";
            if (f.ShowDialog() == true)
            {
                filePathUploadTextBox.Text = f.FileName;
            }
        }


        //private void GridMain_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        //    var songs = mainWindowViewModel.SongsList;

        //    if (!((bool)e.NewValue))
        //    {

        //        mainWindowViewModel.SongsList.Clear();


        //    }
        //    // songList.Items.Refresh();

        //}
    }
}
