using System;
using System.Collections.Generic;
using System.Text;

namespace uTag.Util.Id3v23
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

        public Id3V23TagHeader(byte[] rawHeaderBytes)
        {
            RawHeaderBytes = rawHeaderBytes;
            RefreshHeaderInfo();
            SetSize(Size);
        }

        private string version;
        private string identifier;
        private byte[] RawHeaderBytes { get; set; }
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
            Buffer.BlockCopy(RawHeaderBytes, 0, identifierBytes, 0, 3);
            identifier = Encoding.ASCII.GetString(identifierBytes);
        }

        private void GetVersion()
        {
            byte[] versionBytes = new byte[2];
            Buffer.BlockCopy(RawHeaderBytes, 3, versionBytes, 0, 2);
            version = BitConverter.ToString(versionBytes, 0).Replace("-", string.Empty).ToLower();
        }

        private void GetFlags()
        {
            switch (RawHeaderBytes[5])
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
            Buffer.BlockCopy(RawHeaderBytes, 6, sizeBytes, 0, 4);
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
}
