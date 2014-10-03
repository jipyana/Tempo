using System.Diagnostics.Contracts;

using ent = Tempo.Main.Entities;

namespace Tempo.Infrastructure.AudioPlayer.Commands
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
                Contract.Requires(songToPlay != null);

                this.songToPlay = songToPlay;
            }
            private readonly ent::Song songToPlay;

            public void Execute(IAudioPlayer audioPlayer)
            {
                Contract.Requires(audioPlayer != null);

                audioPlayer.Play(songToPlay);
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
