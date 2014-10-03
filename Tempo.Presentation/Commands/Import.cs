using System;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;

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
                    _importCommand = new RelayCommand(importCommandAction());
                }
                return _importCommand;
            }
        }
        private Action importCommandAction()
        {
            return new Action(
                () =>
                {
                    var songsToImport = songImporter.GetAll_fromDirectory();
                    playlist.Add(songsToImport);

                    // Update UI (TODO: Fix, this is an ugly approach)

                    var songsToImport_nonDupilicates = 
                        songsToImport.Where(
                            import => !this.SongsList.Any(
                                songInList => songInList.Uri.ToString() == import.Uri.ToString()
                        )).ToList();
                    foreach (var song in songsToImport)
                    {
                        this.SongsList.Add(song);
                    }
                });
        }
    }
}
