using FluentAssertions;
using ent = Tempo.Main.Entities;

namespace Tempo.Services.AudioPlayer.Commands
{
    public abstract class Commands
    {
        public class Play : IAudioPlayerCommand
        {
            public Play
            (
                ent::Song songToPlay
            )
            {
                songToPlay.Should().NotBeNull();

                this.songToPlay = songToPlay;
            }
            private readonly ent::Song songToPlay;

            public void Execute(IAudioPlayer audioPlayer)
            {
                audioPlayer.Should().NotBeNull();

                audioPlayer.Play(songToPlay);
            }
        }

        public class Pause : IAudioPlayerCommand
        {
            public void Execute(IAudioPlayer audioPlayer)
            {
                audioPlayer.Pause();
            }
        }

      

        public class Stop : IAudioPlayerCommand
        {
            public void Execute(IAudioPlayer audioPlayer)
            {
                audioPlayer.Stop();
            }
        }
    }
}
