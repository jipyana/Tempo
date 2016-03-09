using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tempo.Services.AudioPlayer.Commands;
using ent = Tempo.Main.Entities;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _shuffleCommand;
        public ICommand ShuffleCommand => _shuffleCommand ?? (_shuffleCommand = new RelayCommand(shuffleCommandExecute(), shuffleCommandCanExecute()));

        private Action shuffleCommandExecute()
        {
            return () =>
            {
                audioPlayer.ProcessCommand(new Commands.Stop());
                this.PlayingSong = null;
                this.playlist.Shuffle();
                this.SongsList = new ObservableCollection<ent::Song>(playlist.GetAll());
            };
        }
        private Func<bool> shuffleCommandCanExecute()
        {
            return () => playlist.GetNumberOfSong() > 0;
        }
    }
}
