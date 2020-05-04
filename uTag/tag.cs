using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;

namespace uTag
{
    /// <summary>
    /// 标签帧接口
    /// </summary>
    public interface ITagFrame
    {
        string GetContent();
        void SetContent(string value);
    }

    /// <summary>
    /// 标签类，可派生出各个标准的标签
    /// </summary>
    /// <typeparam name="TFrame">标签帧</typeparam>
    public abstract class Tag<TFrame> where TFrame:ITagFrame
    {
        /// <summary>
        /// 记录常用标签帧关键字，便于快速访问：
        /// 0：标题
        /// 1：艺术家
        /// 2：专辑名
        /// 3：年代
        /// 4：文件格式
        /// </summary>
        private string[] commonKey;

        /// <summary>
        /// 如果想要单独得到标签帧，可使用索引器搜索标签帧关键字得到
        /// </summary>
        /// <param name="tagKey">标签帧关键字</param>
        /// <returns>字符串内容，若不能转换为字符串则报错TagNotStringException</returns>
        public string this[string tagKey]
        {
            get => TagFramesDictionary[tagKey].GetContent();
            set => TagFramesDictionary[tagKey].SetContent(value);
        }

        /// <summary>
        /// 标签帧集合
        /// </summary>
        public Dictionary<string, TFrame> TagFramesDictionary { get; set; }

        /// <summary>
        /// 标题的快速访问方式
        /// </summary>
        public string Title {
            get => this[commonKey[0]];
            set => this[commonKey[0]] = value;
        }

        /// <summary>
        /// 艺术家的快速访问方式
        /// </summary>
        public string Artist
        {
            get => this[commonKey[1]];
            set => this[commonKey[1]] = value;
        }

        /// <summary>
        /// 专辑名称的快速访问方式
        /// </summary>
        public string Album
        {
            get => this[commonKey[2]];
            set => this[commonKey[2]] = value;
        }

        /// <summary>
        /// 年代的快速访问方式
        /// </summary>
        public string Year
        {
            get => this[commonKey[3]];
            set => this[commonKey[3]] = value;
        }

        /// <summary>
        /// 文件格式的快速访问方式
        /// </summary>
        public string Format
        {
            get => this[commonKey[4]];
            set => this[commonKey[4]] = value;
        }

        /// <summary>
        /// 得到专辑封面图片
        /// </summary>
        /// <returns></returns>
        public abstract Bitmap GetPicture();

        /// <summary>
        /// 设置专辑封面图片
        /// </summary>
        public abstract void SetPicture();
    }

}
