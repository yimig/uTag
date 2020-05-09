using System;
using System.Collections.Generic;
using System.Text;

namespace uTag.Util.Flac
{
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
        private byte[] RawFrameBytes { get; set; }
    }
}
