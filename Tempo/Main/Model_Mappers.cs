using System.IO;

using ent = Tempo.Main.Entities;

namespace Tempo.Main.Mappers
{
    public abstract class Song
    {
        public static ent::Song FromFileInfo(FileInfo fileInfo)
        {
            return new ent::Song(fileInfo.Name.Remove(fileInfo.Name.Length - 4), fileInfo.FullName);
        }
    }
}
