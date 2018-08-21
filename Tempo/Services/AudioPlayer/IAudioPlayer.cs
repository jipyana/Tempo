using Tempo.Services.AudioPlayer.Commands;
using ent = Tempo.Main.Entities;

namespace Tempo.Services.AudioPlayer
{
    public delegate void PlaybackEnded();
    public interface IAudioPlayer
    {
        void Play(ent::Song song);
        void Stop();
        void Pause();
        void ProcessCommand(IAudioPlayerCommand command);

        bool        IsPlaying           { get; }
        ent::Song   PlayingSong         { get; }

        event PlaybackEnded OnPlaybackEnded;
    }
}
