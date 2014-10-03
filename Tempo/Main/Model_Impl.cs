using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Forms;
using System.IO;

using ent = Tempo.Main.Entities;
using map = Tempo.Main.Mappers;

namespace Tempo.Main.Model.Impl
{
    public class SongsImporter : ISongsImporter
    {
        public IReadOnlyCollection<ent::Song> GetAll_fromDirectory()
        {
            var path = tryGetDirectoryPath();

            return 
                (path != null)
                    ? getSongsFromPath(path)
                    : new List<ent::Song>();
        }
        private string tryGetDirectoryPath()
        {
            var dialog = new FolderBrowserDialog();
            var hasSelected = dialog.ShowDialog() == DialogResult.OK;

            if(hasSelected)
            {
                return dialog.SelectedPath;
            }
            return null;
        }
        private IReadOnlyCollection<ent::Song> getSongsFromPath(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            Contract.Requires(!string.IsNullOrWhiteSpace(path));

            var directory = new DirectoryInfo(path);

            return
                directory.GetFiles()
                         .Where (fileIsAcceptableByExtension)
                         .Select(map::Song.FromFileInfo)
                         .ToList();
        }
        private bool fileIsAcceptableByExtension(FileInfo fileInfo)
        {
            var acceptableExtensions = new[]{ ".MP3" };

            return acceptableExtensions.Contains(fileInfo.Extension.ToUpper());
        }
    }
    public class DummyPlayList : IPlaylist
    {
        private readonly List<ent::Song> songsInPlaylist = new List<ent.Song>();
        public IReadOnlyCollection<ent.Song> GetAll()
        {
            return songsInPlaylist;
        }

        public void Add(IReadOnlyCollection<ent.Song> newSongs)
        {
            songsInPlaylist.AddRange(newSongs);
        }
    }
}
