using System.Collections.Generic;

using ent = Tempo.Main.Entities;

namespace Tempo.Main.Model
{
    public interface IPlaylist
    {
        List<ent::Song> GetAll();
        ent::Song GetOne_byIndex(int index);
        void Add(IReadOnlyCollection<ent::Song> newSongs);
        int GetIndexOfSong(ent::Song song);
        int GetNumberOfSong();
        void Shuffle();
        void Clear();
    }

    public interface ISongsImporter
    {
        IReadOnlyCollection<ent::Song> GetAll_fromDirectory(); 
    }
}
