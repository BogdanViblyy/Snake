using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NAudio.Wave;
using System.Media;
using NAudio.Wave;


namespace Snake
{
    public class Sounds
    {
        public void PlayBackgroundMusic()
        {

            string musicPath = "roblox-death-sound-effect.wav";


            using (var audioFile = new AudioFileReader(musicPath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();


                Thread.Sleep(Timeout.Infinite);
            }
        }
    }
}