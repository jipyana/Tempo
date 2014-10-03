using System;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;

using Tempo.Infrastructure.AudioPlayer.Commands;

namespace Tempo.Presentation
{
    public partial class MainWindowViewModel
    {
        private ICommand _playCommand;
        public ICommand PlayCommand
        {
            get
            {
                if (_playCommand == null)
                {
                    _playCommand = new RelayCommand(playCommandExecute(),playCommandCanExecute());
                }
                return _playCommand;
            }
        }
        private Action playCommandExecute()
        {
            return new Action(
                () =>
                {
                    audioPlayer.ProcessCommand(
                        command: new Commands.Play(songToPlay: this.SelectedSong)
                    );
                    this.PlayingSong = this.SelectedSong;
                });
        }
        private Func<bool> playCommandCanExecute()
        {
            return new Func<bool>(
                () =>
                {
                    return this.SelectedSong != null;
                });
        }
    }
}
