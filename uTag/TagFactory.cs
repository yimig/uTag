using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using uTag.Util;

namespace uTag
{
    public static class TagFactory
    {
        public static ITag Load(FileInfo file)
        {
            return new Id3V23Tag(file);
        }
    }
}
