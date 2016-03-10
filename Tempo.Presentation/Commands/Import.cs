using System;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _importCommand;
        public ICommand ImportCommand => _importCommand ?? (_importCommand = new RelayCommand(importCommandAction()));

        private Action importCommandAction()
        {
            return () =>
            {
                var songsToImport = songImporter.GetAll_fromDirectory();
                playlist.Add(songsToImport);

                var songsToImport_nonDupilicates = 
                    songsToImport.Where(
                        import => this.SongsList.All(songInList => songInList.Uri.ToString() != import.Uri.ToString())
                        ).ToList();
                foreach (var song in songsToImport_nonDupilicates)
                {
                    this.SongsList.Add(song);
                }
            };
        }
    }
}
