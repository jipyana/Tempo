using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private ObservableCollection<string> songName;
        private ObservableCollection<string> selectedSong;

        //public IEnumerable<Song> GetSongs()
        //{
        //}
        //private readonly ViewModel.MainWindowViewModel vm;
        public AddPlaylist()
        {
            InitializeComponent();
           // vm = new ViewModel.MainWindowViewModel();
            //this.DataContext = vm;
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddSong_Click(object sender, RoutedEventArgs e)
        {
           
            //newPlaylist.add
        }

        private void RemoveSong_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
