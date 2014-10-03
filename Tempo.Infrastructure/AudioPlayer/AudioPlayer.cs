﻿using System.Diagnostics.Contracts;

using NAudio.Wave;

using Tempo.Infrastructure.AudioPlayer.Commands;

using ent = Tempo.Main.Entities;

namespace Tempo.Infrastructure.AudioPlayer
{
    public class AudioPlayer : IAudioPlayer
    {
        public AudioPlayer
        (
        )
        {
            this.Stop();
        }
        private WaveOut       waveOut { get; set; }
        private Mp3FileReader reader  { get; set; }


        public void Play(ent::Song song)
        {
            Contract.Requires(song != null);
            this.Stop();

            waveOut    = new WaveOut();
            var reader = new Mp3FileReader(song.Uri.ToString());
            waveOut.Init(reader);
            waveOut.Play();

            this.IsPlaying   = true;
            this.PlayingSong = song;
        }

        public void Stop()
        {
            if(waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
            this.IsPlaying   = false;
            this.PlayingSong = null;
        }

        public void ProcessCommand(IAudioPlayerCommand command)
        {
            command.Execute(this);
        }


        public bool      IsPlaying      { get; private set; }
        public ent::Song PlayingSong { get; private set; }

    }
}