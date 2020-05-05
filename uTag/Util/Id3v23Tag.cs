using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace uTag.Util
{
    public class Id3V23TagHeader : ITagHeader
    {

        public string Version { get; set; }
        public string Length { get; set; }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }


    public class Id3V23TagFrame:ITagFrame
    {
        public string GetContent()
        {
            throw new NotImplementedException();
        }

        public void SetContent(string value)
        {
            throw new NotImplementedException();
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }

    public class Id3V23Tag:Tag<Id3V23TagHeader,Id3V23TagFrame>
    {
        public Id3V23Tag(FileInfo file)
        {

        }

        public override string Title { get; set; }
        public override string Artist { get; set; }
        public override string Album { get; set; }
        public override string Year { get; set; }
        public override string Format { get; set; }
        public override Bitmap Picture { get; set; }

        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}
