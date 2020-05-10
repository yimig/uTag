using System;
using System.Collections.Generic;
using System.Text;

namespace uTag.Util.Flac
{
    public class FlacTagFrame : ITagFrame
    {
        public FlacTagFrame(string frameStr)
        {
            FrameStr = frameStr;
            string[] tempStr = FrameStr.Split(new char[] {'='});
            if (tempStr.Length == 2)
            {
                Id = tempStr[0];
                Content = tempStr[1];
            }
            else if (tempStr.Length>1)
            {
                Id = tempStr[0];
                Content=String.Empty;
            }
            else
            {
                throw new TagAnalyseException();
            }
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        private string FrameStr { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }
    }
}
