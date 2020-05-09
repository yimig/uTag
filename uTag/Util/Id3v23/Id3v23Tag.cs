using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace uTag.Util.Id3v23
{
    public class Id3V23Tag : Tag<Id3V23TagHeader, Id3V23TagFrame>
    {
        /// <summary>
        /// Create id3v2.3 tag,may course IOException
        /// </summary>
        /// <param name="file"></param>
        public Id3V23Tag(FileInfo file)
        {
            TagFramesDict = new Dictionary<string, Id3V23TagFrame>();
            BinaryReader br = new BinaryReader(new FileStream(file.FullName, FileMode.Open));
            var rawTagHeader = br.ReadBytes(10);
            TagHeader = new Id3V23TagHeader(rawTagHeader);
            initFramesDict(br.ReadBytes(TagHeader.Size));
        }

        public override string Title
        {
            get
            {
                if (TagFramesDict.Keys.Contains("TIT1")) return this["TIT1"];
                else
                {
                    return this["TIT2"];
                }
            }
            set
            {

            }
        }

        public override string Artist
        {
            get
            {
                if (TagFramesDict.Keys.Contains("TPE1")) return this["TPE1"];
                else
                {
                    return this["TPE2"];
                }
            }
            set
            {

            }
        }

        public override string Album
        {
            get => this["TALB"];
            set
            {

            }
        }

        public override string Year
        {
            get => this["TYER"];
            set
            {

            }
        }

        public override string Format
        {
            get
            {
                return "ID3V2.3";
            }
        }

        public override byte[] Picture
        {
            get => ((Id3V23TagFrame) TagFramesDict["APIC"]).GetImage();
            set
            {

            }
        }

        public override string TrackNumber
        {
            get => this["TRCK"];
            set
            {

            }
        }

        public override string Genre
        {
            get => this["TCON"];
            set
            {

            }
        }

        public override List<Id3V23TagFrame> TagFramesFactory(byte[] rawFramesBytes)
        {
            List<Id3V23TagFrame> frames = new List<Id3V23TagFrame>();
            for (int i = 0; ;)
            {
                byte[] rawFrameHeaderBytes = new byte[4];
                Buffer.BlockCopy(rawFramesBytes, i + 4, rawFrameHeaderBytes, 0, 4);
                int sizeCount = BitConverter.ToInt32(rawFrameHeaderBytes.Reverse().ToArray(), 0) + 10;
                if (sizeCount + i + 4 < rawFramesBytes.Length)
                {
                    byte[] rawFrameBytes = new byte[sizeCount];
                    Buffer.BlockCopy(rawFramesBytes, i, rawFrameBytes, 0, sizeCount);
                    var frameID = GetFrameId(rawFrameBytes);
                    if (frameID != "\0\0\0\0") frames.Add(new Id3V23TagFrame(rawFrameBytes,frameID));
                    else
                    {
                        break;
                    }
                    i += sizeCount;
                }
                else
                {
                    break;
                }
            }

            return frames;
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }

        private string GetFrameId(byte[] rawTagFrameBytes)
        {
            byte[] idBytes = new byte[4];
            Buffer.BlockCopy(rawTagFrameBytes, 0, idBytes, 0, 4);
            return Encoding.ASCII.GetString(idBytes);
        }

        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}
