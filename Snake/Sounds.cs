using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NAudio.Wave;
using System.Media;


public class Sounds
{


    public void PlayDeathSound()
    {
        string deathSoundPath = "C:\\Users\\Administrator\\source\\repos\\Snake\\Snake\\roblox-death-sound-effect.mp3";

        using (var audioFile = new AudioFileReader(deathSoundPath))
        using (var outputDevice = new WaveOutEvent())
        {
            outputDevice.Init(audioFile);
            outputDevice.Play();
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(100);  // Wait until the sound has finished playing
            }
        }
    }
}