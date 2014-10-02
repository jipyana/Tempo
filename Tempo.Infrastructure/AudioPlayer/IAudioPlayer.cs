using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ent = Tempo.Main.Entities;

namespace Tempo.Infrastructure.AudioPlayer
{
    public interface IAudioPlayer
    {
        void Play(ent::Song song);
    }
}
