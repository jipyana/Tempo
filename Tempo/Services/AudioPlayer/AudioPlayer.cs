using FluentAssertions;
using NAudio.Wave;
using Tempo.Services.AudioPlayer.Commands;
using ent = Tempo.Main.Entities;

namespace Tempo.Services.AudioPlayer
{
    public class AudioPlayer : IAudioPlayer
    {
        public AudioPlayer
        (
        )
        {
            this.IsPlaying = false;
        }
        private WaveOut       waveOut { get; set; }
        private Mp3FileReader reader  { get; set; }

        public bool         IsPlaying           { get; private set; }
        public ent::Song    PlayingSong         { get; private set; }

        public event PlaybackEnded OnPlaybackEnded;


        public void Play(ent::Song song)
        {
            song.Should().NotBeNull();
            this.Stop();

            waveOut    = new WaveOut();
            var mp3FileReader = new Mp3FileReader(song.Uri);
            waveOut.Init(mp3FileReader);
            waveOut.Play();
            waveOut.PlaybackStopped += (sender, args) => OnPlaybackEnded?.Invoke();

            this.IsPlaying   = true;
            this.PlayingSong = song;
        }

        public void Stop()
        {
            if(waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
            this.IsPlaying   = false;
            this.PlayingSong = null;
        }

        public void ProcessCommand(IAudioPlayerCommand command)
        {
            command.Execute(this);
        }

        ~AudioPlayer()
        {
            this.waveOut?.Dispose();
        }
    }
}
