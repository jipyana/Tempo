using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Forms;
using System.IO;

using ent = Tempo.Main.Entities;
using map = Tempo.Main.Mappers;
using System.Xml.Linq;
using System;

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
        public IReadOnlyCollection<ent.Song> GetAll()
        {
            return songsInPlaylist;
        }
        private readonly List<ent::Song> songsInPlaylist = new List<ent.Song>();


        public void Add(IReadOnlyCollection<ent.Song> newSongs)
        {
            songsInPlaylist.AddRange(newSongs);
        }
    }
    public class XmlPlaylist : IPlaylist
    {
        private class Settings
        {
            public string PlaylistRelativePath { get { return @"Assets\Data\Playlist.xml"; } }
            public string PlaylistAbsolutePath { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PlaylistRelativePath); } }
        }

        public XmlPlaylist
        (
        )
        {
            _settings = new Settings();
        }
        private readonly Settings _settings;

        public IReadOnlyCollection<ent::Song> GetAll()
        {
            try
            {
                var playlist = XDocument.Load(_settings.PlaylistAbsolutePath);

                return 
                    playlist
                    .Descendants("song")
                    .Select(x => new{
                        Name = (string)x.Element("name"),
                        Uri  = (string)x.Element("uri")
                    })
                    .Select(x => new ent::Song(name: x.Name, uri: x.Uri))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<ent::Song>();
            }
        }

        public void Add(IReadOnlyCollection<ent::Song> newSongs)
        {
            foreach (var newSong in newSongs)
            {
                XDocument playlist;
                try
                {
                    playlist = XDocument.Load(_settings.PlaylistAbsolutePath);
                }
                catch (Exception)
                {
                    playlist = new XDocument();
                    playlist.Add(new XElement("record"));
                }

                playlist
                    .Element("record").Add(
                        new XElement("song",
                            new XElement("name", newSong.Name),
                            new XElement("uri", newSong.Uri)
                        )
                    );

                playlist.Save(_settings.PlaylistAbsolutePath);
            }
        }
    }
}
