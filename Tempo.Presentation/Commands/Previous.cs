using System;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;

using Tempo.Infrastructure.AudioPlayer.Commands;

namespace Tempo.Presentation
{
    public partial class MainWindowViewModel
    {
        private ICommand _previousCommand;
        public ICommand PreviousCommand
        {
            get
            {
                if (_previousCommand == null)
                {
                    _previousCommand = new RelayCommand(previousCommandExecute(), previousCommandCanExecute());
                }
                return _previousCommand;
            }
        }
        private Action previousCommandExecute()
        {
            return new Action(
                () =>
                {

                });
        }
        private Func<bool> previousCommandCanExecute()
        {
            return new Func<bool>(
                () =>
                {
                    return false;
                });
        }
    }
}
