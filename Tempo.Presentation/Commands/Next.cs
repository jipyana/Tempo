using System;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;

using Tempo.Infrastructure.AudioPlayer.Commands;

namespace Tempo.Presentation
{
    public partial class MainWindowViewModel
    {
        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (_nextCommand == null)
                {
                    _nextCommand = new RelayCommand(nextCommandExecute(), nextCommandCanExecute());
                }
                return _nextCommand;
            }
        }
        private Action nextCommandExecute()
        {
            return new Action(
                () =>
                {
                    var indexOfCurrentSong = playlist.GetIndexOfSong(audioPlayer.PlayingSong);
                    var nextSong = playlist.GetOne_byIndex(indexOfCurrentSong + 1);
                    audioPlayer.ProcessCommand(
                        command: new Commands.Play(songToPlay: nextSong)
                    );
                     this.PlayingSong = nextSong ;
                });
        }
        private Func<bool> nextCommandCanExecute()
        {
            return new Func<bool>(
                () =>
                {
                    if(audioPlayer.PlayingSong != null)
                    {
                        var indexOfCurrentSong = playlist.GetIndexOfSong(audioPlayer.PlayingSong);
                        var isThereNextSong = indexOfCurrentSong + 2 <= playlist.GetNumberOfSong();
                        
                        return  isThereNextSong;
                    }
                    return false;
                });
        }
    }
}
