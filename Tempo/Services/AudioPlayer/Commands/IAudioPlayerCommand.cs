namespace Tempo.Services.AudioPlayer.Commands
{
    public interface IAudioPlayerCommand
    {
        void Execute(IAudioPlayer audioPlayer);
    }
}
