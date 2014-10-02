using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using Tempo.Main.Model.Impl;

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
                    _importCommand = new RelayCommand(() =>
                        {
                            var songsToImport = songImporter.GetAll_fromDirectory();
                            playlist.Add(songsToImport);
                        });
                }
                return _importCommand;
            }
        }
    }
}
