using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tempo.Services.AudioPlayer.Commands;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _playCommand;
        public ICommand PlayCommand => _playCommand ?? (_playCommand = new RelayCommand(playCommandExecute(), playCommandCanExecute()));

        private Action playCommandExecute()
        {
            return () =>
            {
                audioPlayer.ProcessCommand(new Commands.Play(songToPlay: this.SelectedSong));
                this.PlayingSong = this.SelectedSong;
                this.ProgressBarMax = Tempo.Services.AudioPlayer.AudioPlayer.GetSongLength(this.SelectedSong);
                // mainWindow timer reset
                // mainWindow timer start
            };
        }
        private Func<bool> playCommandCanExecute()
        {
            return () => this.SelectedSong != null;
        }
    }
}
