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
using Tempo.Main.Entities;
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


        // kaden.ghostsofutah.com:9578/music/help
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            String title = titleTextBox.Text;
            String artist = artistTextBox.Text;
            String genre = genreTextBox.Text;

            //string json = string.Empty;
            //string httpRequestString = $"kaden.ghostsofutah.com:9578/music/getSongs/title={title}&artist={artist}&genre={genre}";
            //Console.WriteLine(httpRequestString);
            ////HTTP request with search parameters
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpRequestString);
            //request.AutomaticDecompression = DecompressionMethods.GZip;

            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //using (Stream stream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    json = reader.ReadToEnd();
            //}

            //// Convert JSON into ArrayList<Song>        
            //List<Song> songs = ConvertSongsFromJSON(json);

            //foreach (Song s in songs)
            //{
            //    TableRow tableRow = new TableRow();
            //    tableRow.Cells.Add(new TableCell(new Paragraph(new Run("100 Letters"))));
            //    tableRow.Cells.Add(new TableCell(new Paragraph(new Run("Halsey"))));
            //    tableRow.Cells.Add(new TableCell(new Paragraph(new Run("Alternative"))));
            //    tableRow.Cells.Add(new TableCell(new Paragraph(new Run("0:3:30"))));
            //    tableRow.Cells.Add(new TableCell(new Paragraph(new Run("0"))));
            //    cloudLibraryTable.RowGroups[0].Rows.Add(tableRow);
            //}
            // Put all of them into table


        }

        private List<Song> ConvertSongsFromJSON(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(json);
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
