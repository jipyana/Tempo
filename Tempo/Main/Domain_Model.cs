using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ent = Tempo.Main.Entities;

namespace Tempo.Main.Model
{
    public interface IPlaylist
    {
        IReadOnlyCollection<ent::Song> GetAll();
        void Add(ent::Song newSong);
    }
}
