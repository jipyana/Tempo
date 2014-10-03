﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
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
                    _playCommand = new RelayCommand(playCommandAction());
                }
                return _playCommand;
            }
        }
        private Action playCommandAction()
        {
            return new Action(
                () =>
                {
                    if(this.SelectedSong != null) 
                    { 
                        audioPlayer.ProcessCommand(
                            command: new Commands.Play(songToPlay: this.SelectedSong)
                        );
                    }
                });
        }
    }
}
