using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using uTag.Util;

namespace uTag
{
    public static class TagFactory
    {
        /// <summary>
        /// 载入音乐文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static ITag Load(FileInfo file)
        {
            ITag resTag;
            switch (file.Extension.ToLower())
            {
                case ".mp3": resTag = new Id3V23Tag(file);
                    break;
                case ".flac":resTag=new FlacTag(file);
                    break;
                default: throw new CannotDetectTagFormatException();
            }

            return resTag;
        }

        /// <summary>
        /// 载入音乐文件
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns></returns>
        public static ITag Load(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            return Load(file);
        }
    }
}