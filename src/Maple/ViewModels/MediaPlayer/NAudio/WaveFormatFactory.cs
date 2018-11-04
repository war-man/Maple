﻿using System.IO;

using NAudio.Wave;

namespace Maple
{
    public static class WaveFormatFactory
    {
        public static WaveFormat GetWaveFormat(string fileName)
        {
            using (var reader = new WaveFileReader(fileName))
            {
                return reader.WaveFormat;
            }
        }

        public static WaveFormat GetWaveFormat(Stream stream)
        {
            using (var reader = new WaveFileReader(stream))
            {
                return reader.WaveFormat;
            }
        }
    }
}
