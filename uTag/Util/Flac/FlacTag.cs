using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace uTag.Util.Flac
{
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

        public override void Save()
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
        public override byte[] Picture { get; set; }
        public override string TrackNumber { get; set; }
        public override string Genre { get; set; }
    }
}
