﻿using System;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Tempo.Services.AudioPlayer.Commands;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _stopCommand;
        public ICommand StopCommand => _stopCommand ?? (_stopCommand = new RelayCommand(stopCommandExecute(), stopCommandCanExecute()));

        private Action stopCommandExecute()
        {
            return () =>
            {
                audioPlayer.ProcessCommand(
                    command: new Commands.Stop()
                    );
                this.PlayingSong = null;
            };
        }
        private Func<bool> stopCommandCanExecute()
        {
            return () => audioPlayer.IsPlaying;
        }
    }
}
