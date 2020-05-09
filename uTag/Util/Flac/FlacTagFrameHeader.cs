using System;
using System.Collections.Generic;
using System.Text;

namespace uTag.Util.Flac
{
    public class FlacTagFrameHeader:ITagSection
    {
        public FlacTagFrameHeader(byte[] rawFlacTagFrameHeader)
        {
            RawFlacTagFrameHeaderBytes = rawFlacTagFrameHeader;
            CheckIsLastMetaData();
            GetType();
            GetFrameSize();
        }

        private void CheckIsLastMetaData()
        {
            IsLastMetaData = RawFlacTagFrameHeaderBytes[0]>>7 == '0';
        }

        private void GetType()
        {
            switch (RawFlacTagFrameHeaderBytes[0]&127)
            {
                case 0: Type = BlockType.STREAMINFO;break;
                case 1: Type = BlockType.PADDING;break;
                case 2: Type = BlockType.APPLICATION;break;
                case 3: Type = BlockType.SEEKTABLE; break;
                case 4: Type = BlockType.VORBIS_COMMEN; break;
                case 5: Type = BlockType.CUESHEET;break;
                case 6: Type = BlockType.PICTURE;break;
                case 127: Type = BlockType.Reserved;break;
                default: Type = BlockType.Broken;break;
            }
        }

        private void GetFrameSize()
        {
            BlockSize = RawFlacTagFrameHeaderBytes[1] << 16 | RawFlacTagFrameHeaderBytes[2] << 8 |
                        RawFlacTagFrameHeaderBytes[3];
        }

        public bool IsLastMetaData { get; set; }
        public BlockType Type { get; set; }
        public int BlockSize { get; set; }
        private byte[] RawFlacTagFrameHeaderBytes { get; set; }
        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        public enum BlockType
        {
            STREAMINFO, PADDING, APPLICATION, SEEKTABLE, VORBIS_COMMEN, CUESHEET, PICTURE, Reserved,Broken
        }
    }
}
