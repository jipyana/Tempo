using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Tempo.Presentation.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand _togglePlaybackLoopCommand;
        public ICommand TogglePlaybackLoopCommand => _togglePlaybackLoopCommand ?? (_togglePlaybackLoopCommand = new RelayCommand(togglePlaybackLoopCommandExecute(), togglePlaybackLoopCommandCanExecute()));

        private Action togglePlaybackLoopCommandExecute()
        {
            return () =>
            {
                this.IsPlaybackLoopOn = !this.IsPlaybackLoopOn;

                var statusText = IsPlaybackLoopOn ? "On" : "Off";
                PlaybackLoopText = $"Looping: {statusText}";
            };
        }
        private Func<bool> togglePlaybackLoopCommandCanExecute()
        {
            return () => true;
        }
    }
}
