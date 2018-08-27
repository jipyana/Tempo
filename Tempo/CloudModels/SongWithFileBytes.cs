using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tempo.CloudModels
{
    class SongWithFileBytes
    {
        public Song song;

        public byte[] fileBytes;

        public SongWithFileBytes(Song s, byte[] bytes)
        {
            song = s;
            fileBytes = bytes;
        }
    }
}
