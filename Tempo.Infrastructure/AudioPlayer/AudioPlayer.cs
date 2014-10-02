using NAudio.Wave;

using ent = Tempo.Main.Entities;

namespace Tempo.Infrastructure.AudioPlayer
{
    public class AudioPlayer : IAudioPlayer
    {
        public void Play(ent::Song song)
        { 
            var reader = new Mp3FileReader(song.Uri.ToString());
            var waveOut = new WaveOut();
            waveOut.Init(reader);
            waveOut.Play();
        }

        private class State
        {
            public State()
            {
                
            }
        }
    }
}
