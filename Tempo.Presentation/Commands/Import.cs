﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace Tempo.Presentation
{
    public partial class MainWindowViewModel
    {
        private ICommand _importCommand;
        public ICommand ImportCommand
        {
            get
            {
                if (_importCommand == null)
                {
                    _importCommand = new RelayCommand(_importCommandAction);
                }
                return _importCommand;
            }
        }
        private readonly Action _importCommandAction = 
            () =>
            {
                var songsToImport = songImporter.GetAll_fromDirectory();
                playlist.Add(songsToImport);

                // Update UI (TODO: Fix, this is an ugly approach)
                this.SongsList.Clear();
                foreach (var song in songsToImport)
                {
                    this.SongsList.Add(song);
                }
            };
    }
}
