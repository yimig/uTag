using System;
using System.Collections.Generic;
using System.Text;

namespace uTag.Util.Flac
{
    public class FlacTagPicture:ITagSection
    {
        public FlacTagPicture(byte[] rawPictureBytes)
        {
            RawPictureBytes = rawPictureBytes;
            InitProperty();
        }

        private void InitProperty()
        {
            GetPictureType();
            GetMime();
            GetDescription();
            GetWidth();
            GetHeight();
            GetDepth();
            GetIndexPictureColors();
            GetSize();
            GetContent();
        }

        private void GetPictureType()
        {
            PictureType = RawPictureBytes[0] << 24 | RawPictureBytes[1] << 16 | RawPictureBytes[2] << 8 |
                          RawPictureBytes[3];
        }

        private void GetMime()
        {
            MimeSize= RawPictureBytes[4] << 24 | RawPictureBytes[5] << 16 | RawPictureBytes[6] << 8 |
                        RawPictureBytes[7];
            byte[] mimeBytes=new byte[MimeSize];
            Buffer.BlockCopy(RawPictureBytes,8,mimeBytes,0,MimeSize);
            Mime = Encoding.ASCII.GetString(mimeBytes);
        }

        private void GetDescription()
        {
            DescriptionSize = RawPictureBytes[MimeSize+8] << 24 | RawPictureBytes[MimeSize + 9] << 16 | RawPictureBytes[MimeSize + 10] << 8 |
                         RawPictureBytes[MimeSize +11];
            byte[] descriptionBytes = new byte[DescriptionSize];
            Buffer.BlockCopy(RawPictureBytes, MimeSize+12, descriptionBytes, 0, DescriptionSize);
            Description = Encoding.ASCII.GetString(descriptionBytes);
            Offset = DescriptionSize + MimeSize + 12;
        }

        private void GetWidth()
        {
            Width = RawPictureBytes[Offset] << 24 | RawPictureBytes[Offset + 1] << 16 | RawPictureBytes[Offset + 2] |
                     RawPictureBytes[Offset + 3];
        }

        private void GetHeight()
        {
            Height=RawPictureBytes[Offset+4] << 24 | RawPictureBytes[Offset + 5] << 16 | RawPictureBytes[Offset + 6] |
                RawPictureBytes[Offset + 7];
        }

        private void GetDepth()
        {
            Depth= RawPictureBytes[Offset+8] << 24 | RawPictureBytes[Offset + 9] << 16 | RawPictureBytes[Offset + 10] |
                   RawPictureBytes[Offset + 11];
        }

        private void GetIndexPictureColors()
        {
            IndexPictureColors= RawPictureBytes[Offset+12] << 24 | RawPictureBytes[Offset + 13] << 16 | RawPictureBytes[Offset + 14] |
                                RawPictureBytes[Offset + 15];
        }

        private void GetSize()
        {
            Size= RawPictureBytes[Offset+16] << 24 | RawPictureBytes[Offset + 17] << 16 | RawPictureBytes[Offset + 18] |
                  RawPictureBytes[Offset + 19];
        }

        private void GetContent()
        {
            Content=new byte[Size];
            Buffer.BlockCopy(RawPictureBytes,Offset+20,Content,0,Size);
        }

        private int Offset { get; set; }
        public int MimeSize { get; set; }
        public int DescriptionSize { get; set; }
        public byte[] RawPictureBytes { get; set; }
        public int PictureType { get; set; }
        public string Mime { get; set; }
        public string Description { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public int IndexPictureColors { get; set; }
        public int Size { get; set; }
        public byte[] Content { get; set; }
        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}
