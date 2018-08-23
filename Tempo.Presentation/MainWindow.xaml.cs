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

      
    }
}
