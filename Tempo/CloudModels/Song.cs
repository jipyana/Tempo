using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tempo.CloudModels
{
    public class Song
    {
        public int id;

        public String title;

        public String artist;

        public String genre;

        public String filePath;

        public int hours;

        public int minutes;

        public int seconds;

        public int fileSize;

        public Song()
        {

        }

        public Song(Song s)
        {
            this.id = s.id;
            this.title = s.title;
            this.artist = s.artist;
            this.genre = s.genre;
            this.filePath = s.filePath;
            this.hours = s.hours;
            this.minutes = s.minutes;
            this.seconds = s.seconds;
            this.fileSize = s.fileSize;
        }

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public String getTitle()
        {
            return title;
        }

        public void setTitle(String title)
        {
            this.title = title;
        }

        public String getArtist()
        {
            return artist;
        }

        public void setArtist(String artist)
        {
            this.artist = artist;
        }

        public String getGenre()
        {
            return genre;
        }

        public void setGenre(String genre)
        {
            this.genre = genre;
        }

        public int getHours()
        {
            return hours;
        }

        public void setHours(int hours)
        {
            this.hours = hours;
        }

        public int getMinutes()
        {
            return minutes;
        }

        public void setMinutes(int minutes)
        {
            this.minutes = minutes;
        }

        public int getSeconds()
        {
            return seconds;
        }

        public void setSeconds(int seconds)
        {
            this.seconds = seconds;
        }

        public int getFileSize()
        {
            return fileSize;
        }

        public void setFileSize(int fileSize)
        {
            this.fileSize = fileSize;
        }

        public String getFilePath()
        {
            return filePath;
        }

        public void setFilePath(String filePath)
        {
            this.filePath = filePath;
        }

        public String toString()
        {
            return "[Song: [id: " + id + "], [title: " + title + "], [artist: " + artist + "]]";
        }
    }
}
