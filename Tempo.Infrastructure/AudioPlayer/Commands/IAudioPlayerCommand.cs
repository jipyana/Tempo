namespace Tempo.Infrastructure.AudioPlayer.Commands
{
    public interface IAudioPlayerCommand
    {
        void Execute(IAudioPlayer audioPlayer);
    }
}
