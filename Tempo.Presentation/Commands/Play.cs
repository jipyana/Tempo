using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tempo.Services.AudioPlayer.Commands;
using Tempo.Presentation.Properties;

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
                
                if(!audioPlayer.IsPaused)
                {
                    this.ProgressBarValue = 0;
                    Tempo.Presentation.MainWindow.timer.Stop();
                }
                Tempo.Presentation.MainWindow.timer.Start();

            };
        }
        private Func<bool> playCommandCanExecute()
        {
            return () => this.SelectedSong != null;
        }
    }
}
