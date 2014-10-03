using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tempo.Infrastructure.AudioPlayer.Commands
{
    public interface IAudioPlayerCommand
    {
        void Execute(IAudioPlayer audioPlayer);
    }
}
