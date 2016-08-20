using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RockAsh
{
    class WaveFile
    {
        private byte[] byteDataArray1;
        public  byte[] ByteDataArray1 { get { return byteDataArray1; } }
        private byte[,] byteDataArray;
        private int[] intDataArray1;
        private int[,] intDataArray;
        private short[] shortDataArray1;
        private short[,] shortDataArray;
        private int length;
        // static int channels;
        private int sampleRate;
        public int SampleRate { get { return sampleRate; } }
        // static int BitsperSample;
        private int DataLength;


        private string ChunkId;
        private int ChunkSize;
        private string Format;

        private string subChunkId;
        private int subChunkSize;
        private short AudioFormat;
        private short channels;
        public short Channels { get { return channels; } }

        private int ByteRate;
        private short BlockAlign;
        private short BitsPerSample;
        public short GETBitsPerSample { get { return BitsPerSample; } }

        private string subChunk2ID;
        private int subChunk2Size;







        public void WaveHeaderIN(string spath)
        {
            FileStream fs = new FileStream(spath, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);




            fs.Position = 0;
            ChunkId = string.Concat(br.ReadChars(4));
            fs.Position = 4;
            ChunkSize = br.ReadInt32();
            fs.Position = 8;
            Format = string.Concat(br.ReadChars(4));

            fs.Position = 12;

            subChunkId = string.Concat(br.ReadChars(4));
            fs.Position = 16;
            subChunkSize = br.ReadInt32();
            fs.Position = 20;
            AudioFormat = br.ReadInt16();
            fs.Position = 22;
            channels = br.ReadInt16();
            fs.Position = 24;
            sampleRate = br.ReadInt32();
            fs.Position = 28;
            ByteRate = br.ReadInt32();
            fs.Position = 32;
            BlockAlign = br.ReadInt16();
            fs.Position = 34;
            BitsPerSample = br.ReadInt16();


            fs.Position = 36;
            subChunk2ID = String.Concat(br.ReadChars(4));

            fs.Position = 40;
            subChunk2Size = br.ReadInt32();

            DataLength = (int)((fs.Length - 44));

            fs.Position = 0;
            byteDataArray1 = new byte[fs.Length - 44];
            fs.Read(byteDataArray1, 44, byteDataArray1.Length - 44);



        }


    }


}
