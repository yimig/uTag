using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace uTag.Util.Flac
{
    public class FlacTagHeader : ITagHeader
    {
        public FlacTagHeader(byte[] rawTagHeader)
        {
            byte[] tempBytes=new byte[38];
            Buffer.BlockCopy(rawTagHeader,4,tempBytes,0,38);
            RawHeaderBytes = tempBytes;
            InitProperty();
        }

        private void InitProperty()
        {
            BuildFrameHeader();
            Size = FrameHeader.BlockSize;
            GetMinBlockSize();
            GetMaxBlockSize();
            GetMinFrameSize();
            GetMaxFrameSize();
            GetSamplingRate();
            GetSoundChannels();
            GetSamplingDigit();
            GetSbitsInOneChannel();
            GetBeforeEncodingMD5();
        }

        private void BuildFrameHeader()
        {
            byte[] tempBytes=new byte[4];
            Buffer.BlockCopy(RawHeaderBytes,0,tempBytes,0,4);
            FrameHeader=new FlacTagFrameHeader(tempBytes);
        }

        private void GetMinBlockSize()
        {
            MinBlockSize = RawHeaderBytes[4] << 8 | RawHeaderBytes[5];
        }

        private void GetMaxBlockSize()
        {
            MaxBlockSize = RawHeaderBytes[6] << 8 | RawHeaderBytes[7];
        }

        private void GetMinFrameSize()
        {
            MinFrameSize = RawHeaderBytes[8] << 16 | RawHeaderBytes[9] << 8 | RawHeaderBytes[10];
        }

        private void GetMaxFrameSize()
        {
            MaxFrameSize= RawHeaderBytes[11] << 16 | RawHeaderBytes[12] << 8 | RawHeaderBytes[13];
        }

        private void GetSamplingRate()
        {
            SamplingRate = RawHeaderBytes[14] << 12 | RawHeaderBytes[15] << 4 | RawHeaderBytes[16] >> 4;
        }

        private void GetSoundChannels()
        {
            SoundChannels = ((RawHeaderBytes[16] & 14) >> 1) + 1;
        }

        private void GetSamplingDigit()
        {
            SamplingDigit = ((RawHeaderBytes[16] & 1) << 4 | RawHeaderBytes[17] >> 4) + 1;
        }

        private void GetSbitsInOneChannel()
        {
            SbitsInOneChannel = (RawHeaderBytes[17] & 15) << 32 | RawHeaderBytes[18] << 24 | RawHeaderBytes[19] << 16 |
                                RawHeaderBytes[20] << 8 | RawHeaderBytes[21];
        }

        private void GetBeforeEncodingMD5()
        {
            byte[] tempBytes=new byte[16];
            Buffer.BlockCopy(RawHeaderBytes,22,tempBytes,0,16);
            BeforeEncodingMD5=BitConverter.ToString(tempBytes, 0).Replace("-", string.Empty).ToLower();
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        public FlacTagFrameHeader FrameHeader { get; set; }
        /// <summary>
        /// 最小块信息
        /// </summary>
        public int MinBlockSize { get; set; }
        /// <summary>
        /// 最大块信息
        /// </summary>
        public int MaxBlockSize { get; set; }
        /// <summary>
        /// 最小帧信息
        /// </summary>
        public int MinFrameSize { get; set; }
        /// <summary>
        /// 最大帧信息
        /// </summary>
        public int MaxFrameSize { get; set; }
        /// <summary>
        /// 采样率（Hz）
        /// </summary>
        public int SamplingRate { get; set; }
        /// <summary>
        /// 声道数
        /// </summary>
        public int SoundChannels { get; set; }
        /// <summary>
        /// 采样位
        /// </summary>
        public int SamplingDigit { get; set; }
        /// <summary>
        /// 一个声道的总采样数
        /// </summary>
        public int SbitsInOneChannel { get; set; }
        /// <summary>
        /// 未编码时的原始信号的MD5
        /// </summary>
        public string BeforeEncodingMD5 { get; set; }
        /// <summary>
        /// 标签版本
        /// </summary>
        public string Version => "1";

        /// <summary>
        /// 标签头的大小
        /// </summary>
        public int Size { get; set; }
        private byte[] RawHeaderBytes { get; set; }


    }
}
