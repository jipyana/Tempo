using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tempo.Infrastructure
{
    public class Player
    {
        public void Play()
        { 
            var reader = new Mp3FileReader(@"D:\Glazba Download\_Cajke\Jovan Perisic\Jovan Perisic - Bozija kazna.mp3");
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader); 
            waveOut.Play();
        }
    }
}
