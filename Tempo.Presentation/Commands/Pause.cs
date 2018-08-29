using System;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Tempo.Services.AudioPlayer.Commands;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _pauseCommand;
        public ICommand PauseCommand => _pauseCommand ?? (_pauseCommand = new RelayCommand(pauseCommandExecute(), pauseCommandCanExecute()));

        private Action pauseCommandExecute()
        {
            return () =>
            {
                audioPlayer.ProcessCommand(new Commands.Pause());

                if (audioPlayer.IsPaused && audioPlayer.IsPlaying)
                {
                    Tempo.Presentation.MainWindow.timer.Stop();
                } else
                {
                    Tempo.Presentation.MainWindow.timer.Start();
                }
                // main window timer pause or resume
            };
        }
        private Func<bool> pauseCommandCanExecute()
        {
            return () => this.SelectedSong != null && audioPlayer.IsPlaying;
        }
    }
}