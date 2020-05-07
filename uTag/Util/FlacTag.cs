using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace uTag.Util
{
    public class FlacTagHeader : ITagHeader
    {
        public FlacTagHeader(byte[] rawTagHeader)
        {
            RawHeaderTag = rawTagHeader;
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        private byte[] RawHeaderTag { get; set; }
        public string Version { get; }
        public int Size { get; set; }
    }

    public class FlacTagFrame : ITagFrame
    {
        public FlacTagFrame(byte[] rawFrameTag)
        {
            RawFrameTag = rawFrameTag;
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        private byte[] RawFrameTag { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }
    }

    public class FlacTag:Tag<FlacTagHeader,FlacTagFrame>
    {
        public FlacTag(FileInfo file)
        {
            TagFramesDict = new Dictionary<string,FlacTagFrame>();
            BinaryReader br = new BinaryReader(new FileStream(file.FullName, FileMode.Open));
            var rawTagHeader = br.ReadBytes(10);
            TagHeader = new FlacTagHeader(rawTagHeader);
            initFramesDict(br.ReadBytes(TagHeader.Size));
        }

        public override List<FlacTagFrame> TagFramesFactory(byte[] rawFramesBytes)
        {
            throw new NotImplementedException();
        }

        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        public override string Title { get; set; }
        public override string Artist { get; set; }
        public override string Album { get; set; }
        public override string Year { get; set; }

        public override string Format => "Flac";

        public override Bitmap Picture { get; set; }
    }
}
