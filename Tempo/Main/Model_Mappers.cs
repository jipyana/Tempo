﻿using System.IO;
using ent = Tempo.Main.Entities;

namespace Tempo.Main.Mappers
{
    public abstract class Song
    {
        public static ent::Song FromFileInfo(FileInfo fileInfo)
        {
            var fileExtensionLength = ".mp3".Length;
            var fileNameWithoutExtension = fileInfo.Name.Remove(fileInfo.Name.Length - fileExtensionLength);

            return new ent::Song(fileNameWithoutExtension, fileInfo.FullName);
        }
    }
}
