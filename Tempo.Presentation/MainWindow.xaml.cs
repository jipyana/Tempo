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

namespace Tempo.Presentation
{
    public partial class MainWindow : MetroWindow
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public MainWindow()
        {

            InitializeComponent();
            vm = new ViewModel.MainWindowViewModel();
            this.DataContext = vm;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += timer_Tick;
            timer.Start();
            SearchButton_Click(null, null);

        }

        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    if ((S.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
        //    {
        //        sliProgress.Minimum = 0;
        //        sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
        //        sliProgress.Value = mePlayer.Position.TotalSeconds;
        //    }
        //}
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
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        //private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    GridMain.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        //}
        private readonly ViewModel.MainWindowViewModel vm;

        private void PlaylistElement_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.PlayCommand.Execute(null);
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

        }

        private void ClearCloudTable()
        {
            while(cloudLibraryTable.RowGroups[0].Rows.Count > 1)
            {
                cloudLibraryTable.RowGroups[0].Rows.RemoveAt(1);
            }
        }

        // kaden.ghostsofutah.com:9578/music/help
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ClearCloudTable();
            String title = titleTextBox.Text;
            String artist = artistTextBox.Text;
            String genre = genreTextBox.Text;

            string json = string.Empty;
            string httpRequestString = $"http://kaden.ghostsofutah.com:9578/music/getSongs/title={title}&artist={artist}&genre={genre}";
            // $"http://10.0.0.130:9578/music/getSongs/title={title}&artist={artist}&genre={genre}"; 


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

            foreach (Song s in songs)
            {
                TableRow tableRow = new TableRow();
                tableRow.Cells.Add(new TableCell());
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(s.getTitle()))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(s.getArtist()))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(s.getGenre()))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run("" + s.getHours() + ":" + s.getMinutes() + ":" + s.getSeconds()))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run("" + (double)(s.getFileSize() / 1000.0) + "mb"))));
                cloudLibraryTable.RowGroups[0].Rows.Add(tableRow);
            }
            // Put all of them into table


        }

        private List<Song> ConvertSongsFromJSON(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(json);
        }

        public void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            cloudLibraryDocReader.Visibility = Visibility.Collapsed;
            UploadFormGrid.Visibility = Visibility.Visible;
        }
        private void addPlaylist_Click(object sender, RoutedEventArgs e)
        {
            AddPlaylist k = new AddPlaylist();
            k.Show();
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

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //cloudLibraryTable.Focus = Visibility.Hidden;
            //uploadGrid.Focus = Visibility.Visible;
            //this ^^^ includes all textbox fields and a submit and cancel button

            //First have user select mp3 file
            //Get the song detail strings from pop up window

            string filePath = filePathUploadTextBox.Text;
            if(filePath == "" || filePath == null || filePath.Split('.')[filePath.Split('.').Length - 1] != "mp3")
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

                if(File.Exists(filePath))
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
                    CancelButton_Click(null, null);
                }
                else
                {
                    MessageBox.Show("The specified File does not exist");
                }
                
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
