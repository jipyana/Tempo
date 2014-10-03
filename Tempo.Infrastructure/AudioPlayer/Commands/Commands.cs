using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                this.songToPlay = songToPlay;
            }
            private readonly ent::Song songToPlay;

            public void Execute(IAudioPlayer audioPlayer)
            {
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
