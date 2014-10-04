using Tempo.Infrastructure.AudioPlayer.Commands;

using ent = Tempo.Main.Entities;

namespace Tempo.Infrastructure.AudioPlayer
{
    public interface IAudioPlayer
    {
        void Play(ent::Song song);
        void Stop();
        void ProcessCommand(IAudioPlayerCommand command);

        bool      IsPlaying   { get; }
        ent::Song PlayingSong { get; }
    }
}
