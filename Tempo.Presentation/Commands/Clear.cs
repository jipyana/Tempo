using System;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Tempo.Services.AudioPlayer.Commands;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _clearCommand;
        public ICommand ClearCommand => _clearCommand ?? (_clearCommand = new RelayCommand(clearCommandExecute(), clearCommandCanExecute()));

        private Action clearCommandExecute()
        {
            return () =>
            {
                this.audioPlayer.ProcessCommand(new Commands.Stop());
                this.PlayingSong = null;
                this.playlist.Clear();
                this.SongsList.Clear();
                this.ProgressBarValue = 0;
                Tempo.Presentation.MainWindow.timer.Stop();
            };
        }
        private Func<bool> clearCommandCanExecute()
        {
            return () => this.SongsList.Any();
        }
    }
}
