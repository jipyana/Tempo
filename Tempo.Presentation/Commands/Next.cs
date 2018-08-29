using System;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Tempo.Services.AudioPlayer.Commands;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _nextCommand;
        public ICommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(nextCommandExecute(), nextCommandCanExecute()));

        private Action nextCommandExecute()
        {
            return () =>
            {
                var indexOfCurrentSong = playlist.GetIndexOfSong(audioPlayer.PlayingSong);
                var nextSong = playlist.GetOne_byIndex(indexOfCurrentSong + 1);
                audioPlayer.ProcessCommand(new Commands.Play(songToPlay: nextSong));
                this.PlayingSong = nextSong ;
                this.ProgressBarValue = 0;
                Tempo.Presentation.MainWindow.timer.Stop();
                Tempo.Presentation.MainWindow.timer.Start();
            };
        }
        private Func<bool> nextCommandCanExecute()
        {
            return () =>
            {
                if(audioPlayer.PlayingSong != null)
                {
                    var indexOfCurrentSong = playlist.GetIndexOfSong(audioPlayer.PlayingSong);
                    var isThereNextSong = indexOfCurrentSong + 2 <= playlist.GetNumberOfSong();
                        
                    return  isThereNextSong;
                }
                return false;
            };
        }
    }
}
