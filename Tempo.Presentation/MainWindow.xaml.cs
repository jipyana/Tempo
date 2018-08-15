using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Tempo.Main.Entities;
using Tempo.Presentation.ViewModel;

namespace Tempo.Presentation
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {

            InitializeComponent();
            vm = new ViewModel.MainWindowViewModel();
            this.DataContext = vm;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);
                   

            switch (index)
            {
                case 0:
                    GridMain.Background = Brushes.Aquamarine;
                    songList.Visibility = Visibility.Visible;
                    break;
                case 1:
                    GridMain.Background = Brushes.Beige;
                    
                       
                        songList.Visibility = Visibility.Hidden;
                        

                    
                    //GridMain.IsVisible;

                    break;
                case 2:
                    GridMain.Background = Brushes.CadetBlue;
                    songList.Visibility = Visibility.Hidden;
                    //clear_music_list();
                    break;

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
