using System;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;

using Tempo.Infrastructure.AudioPlayer.Commands;

namespace Tempo.Presentation
{
    public partial class MainWindowViewModel
    {
        private ICommand _stopCommand;
        public ICommand StopCommand
        {
            get
            {
                if (_stopCommand == null)
                {
                    _stopCommand = new RelayCommand(stopCommandExecute(), stopCommandCanExecute());
                }
                return _stopCommand;
            }
        }
        private Action stopCommandExecute()
        {
            return new Action(
                () =>
                {
                    audioPlayer.ProcessCommand(
                        command: new Commands.Stop()
                    );
                    this.PlayingSong = null;
                });
        }
        private Func<bool> stopCommandCanExecute()
        {
            return new Func<bool>(
                () =>
                {
                    return audioPlayer.IsPlaying;
                });
        }
    }
}
