using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

        public MainWindow()
        {

            InitializeComponent();
            vm = new ViewModel.MainWindowViewModel();
            this.DataContext = vm;

            // HTTP request first 100 songs

        }

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

        // kaden.ghostsofutah.com:9578/music/help
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            String title = titleTextBox.Text;
            String artist = artistTextBox.Text;
            String genre = genreTextBox.Text;

            string json = string.Empty;
            string httpRequestString = $"http://10.0.0.130:9578/music/getSongs/title={title}&artist={artist}&genre={genre}";
            
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

            foreach (Song s in songs)
            {
                TableRow tableRow = new TableRow();
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

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            //cloudLibraryTable.Focus = Visibility.Hidden;
            //uploadGrid.Focus = Visibility.Visible;
            //this ^^^ includes all textbox fields and a submit and cancel button

            //First have user select mp3 file
            //Get the song detail strings from pop up window

            string filePath = "";//filePathUploadTextBox.Text;
            string title = "";//titleUploadTextBox.Text;
            string artist = "";//artistUploadTextBox.Text;
            string genre = "";//genreUploadTextBox.Text;
            int hours = 0;// Make some kinda time entry thing for the file
            int minutes = 0;
            int seconds = 0;
            //get filesize from file
            int fileSize = 0; //file.getBytes.Length / 1000;
            Song s = new Song();
            //File file = new File(filePath);

            s.title = title;
            s.artist = artist;
            s.genre = genre;
            s.hours = hours;
            s.minutes = minutes;
            s.seconds = seconds;
            s.fileSize = fileSize;
            //SongWithFileBytes songWithFile = new SongWithFileBytes(s, file.getBytes);

            //convert songWithFile to json
            //send songWithFile to:
            //  http://kaden.ghostsofutah.com:9578/music/
            //  with a post request and the json in the body

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
