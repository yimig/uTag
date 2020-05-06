using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace uTag.Util
{
    public class Id3V23TagHeader : ITagHeader
    {
        /// <summary>
        /// 标签头标识
        /// </summary>
        public enum FlagType
        {
            /// <summary>
            /// 无标识
            /// </summary>
            Normal,
            /// <summary>
            /// 非同步编码
            /// </summary>
            Unsynchronisation,
            /// <summary>
            /// 拓展标签头
            /// </summary>
            Extendedheader,
            /// <summary>
            /// 测试指示位
            /// </summary>
            Experimemtalindicator,
            /// <summary>
            /// 标识损坏
            /// </summary>
            Break
        }

        public Id3V23TagHeader(byte[] rawTagHeader)
        {
            RawTagHeader = rawTagHeader;
            RefreshHeaderInfo();
            SetSize(Size);
        }

        private string version;
        private string identifier;
        public byte[] RawTagHeader { get; set; }
        public int Size { get; set; }
        public FlagType Flags { get; set; }

        public string Version => version;
        public string Identifier => identifier;

        private void RefreshHeaderInfo()
        {
            GetIdentifier();
            GetVersion();
            GetFlags();
            GetSize();
        }

        private void GetIdentifier()
        {
            byte[] identifierBytes = new byte[3];
            Buffer.BlockCopy(RawTagHeader, 0, identifierBytes, 0, 3);
            identifier = Encoding.ASCII.GetString(identifierBytes);
        }

        private void GetVersion()
        {
            byte[] versionBytes = new byte[2];
            Buffer.BlockCopy(RawTagHeader, 3, versionBytes, 0, 2);
            version = BitConverter.ToString(versionBytes, 0).Replace("-", string.Empty).ToLower();
        }

        private void GetFlags()
        {
            switch (RawTagHeader[5])
            {
                case 128:
                    Flags = FlagType.Unsynchronisation;
                    break;
                case 64:
                    Flags = FlagType.Extendedheader;
                    break;
                case 32:
                    Flags = FlagType.Experimemtalindicator;
                    break;
                case 0:
                    Flags = FlagType.Normal;
                    break;
                default:
                    Flags = FlagType.Break;
                    break;
            }
        }

        private void GetSize()
        {
            byte[] sizeBytes = new byte[4];
            Buffer.BlockCopy(RawTagHeader, 6, sizeBytes, 0, 4);
            Size = sizeBytes[0] << 21 | sizeBytes[1] << 14 | sizeBytes[2] << 7 | sizeBytes[3];

        }

        /// <summary>
        /// 设置标签大小（未完成）
        /// </summary>
        /// <param name="size"></param>
        private void SetSize(int size)
        {
            if (size > 268435455)
            {
                throw new TagOverSizeException();
            }
            else
            {
                byte[] sizeBytes = new byte[4];
            }
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }


    public class Id3V23TagFrame : ITagFrame
    {
        public Id3V23TagFrame(byte[] rawTagFrame)
        {
            RawTagFrameBytes = rawTagFrame;
            GetId();
            GetSize();
            GetTagStatue();
            GetContent();
        }

        private byte[] RawTagFrameBytes { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }
        public bool isReadOnly { get; set; }
        public int Size { get; set; }

        private void GetId()
        {
            byte[] idBytes = new byte[4];
            Buffer.BlockCopy(RawTagFrameBytes, 0, idBytes, 0, 4);
            Id = Encoding.ASCII.GetString(idBytes);
        }

        private void GetTagStatue()
        {
            byte[] statueBytes = new byte[2];
            Buffer.BlockCopy(RawTagFrameBytes, 8, statueBytes, 0, 2);
            isReadOnly = (statueBytes[0] & 32) == 32 ? true : false;
        }

        private void GetSize()
        {
            byte[] sizeBytes = new byte[4];
            Buffer.BlockCopy(RawTagFrameBytes, 4, sizeBytes, 0, 4);
            Size = BitConverter.ToInt32(sizeBytes.Reverse().ToArray(), 0);
        }

        private void GetContent()
        {
            byte[] contentBytes = new byte[Size];
            Buffer.BlockCopy(RawTagFrameBytes, 10, contentBytes, 0, Size);
            var encoding = new UnicodeEncoding(true, false);
            var encoding2 = new UTF8Encoding(true);
            var encoding3 = Encoding.GetEncoding("gb2312");
            var encoding4 = Encoding.ASCII;
            var encoding5 = Encoding.Default;
            Content = encoding5.GetString(contentBytes);
        }

        private void SetContent(string value)
        {
            throw new NotImplementedException();
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }

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
                    if (TagFramesDict.Keys.Contains("TIT2")) return this["TIT2"];
                    else return "";
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
                    if (TagFramesDict.Keys.Contains("TPE2")) return this["TPE2"];
                    else return "";
                }
            }
            set
            {

            }
        }

        public override string Album
        {
            get
            {
                if (TagFramesDict.Keys.Contains("TALB")) return this["TALB"];
                else
                {
                    return "";
                }
            }
            set
            {

            }
        }

        public override string Year
        {
            get
            {
                if (TagFramesDict.Keys.Contains("TYER")) return this["TYER"];
                else
                {
                    return "";
                }
            }
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

        public override Bitmap Picture { get; set; }

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
                    var frame = new Id3V23TagFrame(rawFrameBytes);
                    if (frame.Id != "\0\0\0\0") frames.Add(new Id3V23TagFrame(rawFrameBytes));
                    i += sizeCount;
                }
                else
                {
                    break;
                }
            }

            return frames;
        }

        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}
