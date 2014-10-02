using System.Collections.Generic;

using ent = Tempo.Main.Entities;

namespace Tempo.Main.Model
{
    public interface IPlaylist
    {
        IReadOnlyCollection<ent::Song> GetAll();
        void Add(IReadOnlyCollection<ent::Song> newSongs);
    }

    public interface ISongsImporter
    {
        IReadOnlyCollection<ent::Song> GetAll_fromDirectory(); 
    }
}
