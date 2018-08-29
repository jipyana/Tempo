using System;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Tempo.Services.AudioPlayer.Commands;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _previousCommand;
        public ICommand PreviousCommand => _previousCommand ??
                                           (_previousCommand = new RelayCommand(previousCommandExecute(), previousCommandCanExecute()));

        private Action previousCommandExecute()
        {
            return () =>
            {
                var indexOfCurrentSong = playlist.GetIndexOfSong(audioPlayer.PlayingSong);
                var nextSong = playlist.GetOne_byIndex(indexOfCurrentSong - 1);
                audioPlayer.ProcessCommand(new Commands.Play(songToPlay: nextSong));
                this.PlayingSong = nextSong;
                this.ProgressBarValue = 0;
                Tempo.Presentation.MainWindow.timer.Stop();
                Tempo.Presentation.MainWindow.timer.Start();
            };
        }
        private Func<bool> previousCommandCanExecute()
        {
            return () =>
            {
                if(audioPlayer.PlayingSong != null)
                {
                    var indexOfCurrentSong = playlist.GetIndexOfSong(audioPlayer.PlayingSong);
                    var isTherePreviousSong = indexOfCurrentSong - 1 >= 0;
                        
                    return  isTherePreviousSong;
                }
                return false;
            };
        }
    }
}
