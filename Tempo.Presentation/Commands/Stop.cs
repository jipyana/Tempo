using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
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
                    _stopCommand = new RelayCommand(stopCommandAction());
                }
                return _stopCommand;
            }
        }
        private Action stopCommandAction()
        {
            return new Action(
                () =>
                {
                    if(this.SelectedSong != null) 
                    { 
                        audioPlayer.ProcessCommand(
                            command: new Commands.Stop()
                        );
                    }
                });
        }
    }
}
