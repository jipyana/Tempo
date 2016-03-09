using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using FluentAssertions;
using Tempo.Extensions;
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

        public List<ent::Song> GetAll() => songsInPlaylist;

        public void Add(IReadOnlyCollection<ent.Song> newSongs) => songsInPlaylist.AddRange(newSongs);

        public int GetIndexOfSong(ent::Song song) => this.GetAll().IndexOf(song);
        public int GetNumberOfSong() => this.GetAll().Count;
        public void Shuffle()
        {
            this.songsInPlaylist.Shuffle();
        }

        public void Clear() => this.songsInPlaylist.Clear();

        public ent::Song GetOne_byIndex(int index) => songsInPlaylist[index];
    }

    public class XmlPlaylist : IPlaylist
    {
        private class Settings
        {
            public string PlaylistRelativePath => "Playlist.xml";
            public string PlaylistAbsolutePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PlaylistRelativePath);
        }

        public XmlPlaylist
        (
        )
        {
            _settings = new Settings();
        }
        private readonly Settings _settings;

        public List<ent::Song> GetAll()
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
                    CreateNewEmptyPlaylist();
                }

                playlist.Should().NotBeNull();
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
        private void CreateNewEmptyPlaylist()
        {
            using (File.Create(_settings.PlaylistAbsolutePath));
        }

        public int GetIndexOfSong(ent::Song song)
        {
            return
                this.GetAll()
                .Select(x => x.Uri)
                .ToList()
                .IndexOf(song.Uri);
        }
        public int GetNumberOfSong() => this.GetAll().Count;
        public void Shuffle()
        {
            var newPlaylist = this.GetAll();
            newPlaylist.Shuffle();
            CreateNewEmptyPlaylist();
            Add(newPlaylist);
        }

        public void Clear() => this.CreateNewEmptyPlaylist();

        public ent::Song GetOne_byIndex(int index) => this.GetAll()[index];
    }
}
