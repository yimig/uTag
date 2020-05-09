using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTag.Util.Id3v23
{
    public class Id3V23TagFrame : ITagFrame
    {
        public Id3V23TagFrame(byte[] rawTagFrame)
        {
            RawFrameBytes = rawTagFrame;
            GetId();
            GetSize();
            GetTagStatue();
            GetContent();
        }

        public Id3V23TagFrame(byte[] rawTagFrame, string id)
        {
            RawFrameBytes = rawTagFrame;
            this.Id = id;
            GetSize();
            GetTagStatue();
            GetContent();
        }

        private byte[] RawFrameBytes { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }
        public bool isReadOnly { get; set; }
        public int Size { get; set; }

        private void GetId()
        {
            byte[] idBytes = new byte[4];
            Buffer.BlockCopy(RawFrameBytes, 0, idBytes, 0, 4);
            Id = Encoding.ASCII.GetString(idBytes);
        }

        private void GetTagStatue()
        {
            byte[] statueBytes = new byte[2];
            Buffer.BlockCopy(RawFrameBytes, 8, statueBytes, 0, 2);
            isReadOnly = (statueBytes[0] & 32) == 32 ? true : false;
        }

        private void GetSize()
        {
            byte[] sizeBytes = new byte[4];
            Buffer.BlockCopy(RawFrameBytes, 4, sizeBytes, 0, 4);
            Size = BitConverter.ToInt32(sizeBytes.Reverse().ToArray(), 0);
        }

        private void GetContent()
        {
            if (Id == "TYER") Content = AsciiStringConvert(RawFrameBytes);
            else
            {
                Content = UnicodeStringConvert(RawFrameBytes);
            }
        }

        private string UnicodeStringConvert(byte[] rawStringBytes)
        {
            byte[] contentBytes = new byte[Size - 3];
            //去除标签头和一位起始符、两位BOM标识符
            Buffer.BlockCopy(rawStringBytes, 13, contentBytes, 0, Size - 3);
            var encoding = new UnicodeEncoding(false, false);
            string strContent = encoding.GetString(contentBytes);
            //去除一位结束符
            if (strContent.Length != 0 && strContent[strContent.Length - 1] == '\0')
            {
                strContent = strContent.Remove(strContent.Length - 1);
            }
            return strContent;
        }

        private string AsciiStringConvert(byte[] rawStringBytes)
        {
            byte[] contentBytes = new byte[Size];
            Buffer.BlockCopy(rawStringBytes, 10, contentBytes, 0, Size);
            var encoding = new ASCIIEncoding();
            return encoding.GetString(contentBytes);
        }

        /// <summary>
        /// 去除图片前面由两个0字节围成的未知区域
        /// </summary>
        /// <param name="imageTempBytes"></param>
        /// <returns></returns>
        private byte[] ImageWithoutHeader(byte[] imageTempBytes)
        {
            byte[] imageBytes;
            if (imageTempBytes[0] == 0)
            {
                int count = 1;
                for (; imageTempBytes[count] != 0; count++) ;
                count++;
                imageBytes = new byte[imageTempBytes.Length - count];
                Buffer.BlockCopy(imageTempBytes, count, imageBytes, 0, imageTempBytes.Length - count);
            }
            else
            {
                imageBytes = imageTempBytes;
            }

            return imageBytes;
        }

        /// <summary>
        /// 得到图片，只有该标签包含图片时才允许使用该方法
        /// </summary>
        /// <returns></returns>
        public byte[] GetImage()
        {
            byte[] imageFormatBytes = new byte[10];
            Buffer.BlockCopy(RawFrameBytes, 11, imageFormatBytes, 0, 10);
            string imageFormat = Encoding.ASCII.GetString(imageFormatBytes);
            if (imageFormat == "image/jpeg")
            {
                byte[] imageTempBytes = new byte[RawFrameBytes.Length - 21];
                Buffer.BlockCopy(RawFrameBytes, 21, imageTempBytes, 0, RawFrameBytes.Length - 21);
                return ImageWithoutHeader(imageTempBytes);
            }
            else
            {
                throw new TagPictureBadException();
            }
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
}
