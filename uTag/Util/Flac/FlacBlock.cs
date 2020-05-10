using System;
using System.Collections.Generic;
using System.Text;

namespace uTag.Util.Flac
{
    public class FlacBlock:ITagSection
    {
        public FlacBlock(byte[] rawFlacBlock,FlacTagBlockHeader blockHeader)
        {
            RawFlacBlock = rawFlacBlock;
            BlockHeader = blockHeader;
            var encoding= new UTF8Encoding(true);
            Content = encoding.GetString(rawFlacBlock);
        }

        private void InitHeader()
        {
            byte[] headerBytes = new byte[4];
            Buffer.BlockCopy(RawFlacBlock, 0, headerBytes, 0, 4);
            BlockHeader = new FlacTagBlockHeader(headerBytes);
        }

        private byte[] RawFlacBlock { get; set; }
        public FlacTagBlockHeader BlockHeader { get; set; }
        public string Content { get; set; }

        public byte[] ToBytes()
        {
            return RawFlacBlock;
        }
    }
}
