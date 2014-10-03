using System;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;

using Tempo.Infrastructure.AudioPlayer.Commands;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _previousCommand;
        public ICommand PreviousCommand
        {
            get
            {
                if (_previousCommand == null)
                {
                    _previousCommand = new RelayCommand(previousCommandExecute(), previousCommandCanExecute());
                }
                return _previousCommand;
            }
        }
        private Action previousCommandExecute()
        {
            return new Action(
                () =>
                {
                    var indexOfCurrentSong = playlist.GetIndexOfSong(audioPlayer.PlayingSong);
                    var nextSong = playlist.GetOne_byIndex(indexOfCurrentSong - 1);
                    audioPlayer.ProcessCommand(
                        command: new Commands.Play(songToPlay: nextSong)
                    );
                    this.PlayingSong = nextSong;
                });
        }
        private Func<bool> previousCommandCanExecute()
        {
            return new Func<bool>(
                () =>
                {
                    if(audioPlayer.PlayingSong != null)
                    {
                        var indexOfCurrentSong = playlist.GetIndexOfSong(audioPlayer.PlayingSong);
                        var isTherePreviousSong = indexOfCurrentSong - 1 >= 0;
                        
                        return  isTherePreviousSong;
                    }
                    return false;
                });
        }
    }
}
