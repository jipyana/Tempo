using System;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Tempo.Extensions;
using Tempo.Services.AudioPlayer.Commands;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _shuffleCommand;
        public ICommand ShuffleCommand => _shuffleCommand ?? (_shuffleCommand = new RelayCommand(shuffleCommandExecute(), shuffleCommandCanExecute()));

        private Action shuffleCommandExecute()
        {
            return () =>
            {
                audioPlayer.ProcessCommand(
                    command: new Commands.Stop()
                    );
                this.PlayingSong = null;
                this.SongsList.Shuffle();
            };
        }
        private Func<bool> shuffleCommandCanExecute()
        {
            return () => playlist.GetNumberOfSong() > 0;
        }
    }
}
