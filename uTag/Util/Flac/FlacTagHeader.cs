using System;
using System.Collections.Generic;
using System.Text;

namespace uTag.Util.Flac
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
        private byte[] RawHeaderBytes { get; set; }
    }
}
