using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;

namespace uTag
{
    public interface ITag
    {
        byte[] ToBytes();
    }

    /// <summary>
    /// 标签帧接口
    /// </summary>
    public interface ITagFrame:ITag
    {
        string GetContent();
        void SetContent(string value);
    }

    public interface ITagHeader:ITag
    {
        string Version { get; set; }
        string Length { get; set; }
    }

    /// <summary>
    /// 标签类，可派生出各个标准的标签
    /// </summary>
    /// <typeparam name="TFrame">标签帧</typeparam>
    public abstract class Tag<THeader, TFrame> : ITag
        where THeader:ITagHeader
        where TFrame : ITagFrame
    {

        /// <summary>
        /// 如果想要单独得到标签帧，可使用索引器搜索标签帧关键字得到
        /// </summary>
        /// <param name="tagKey">标签帧关键字</param>
        /// <returns>字符串内容，若不能转换为字符串则报错TagNotStringException</returns>
        public string this[string tagKey]
        {
            get => TagFramesDict[tagKey].GetContent();
            set => TagFramesDict[tagKey].SetContent(value);
        }

        public THeader TagHeader { get; set; }

        /// <summary>
        /// 标签帧集合
        /// </summary>
        public Dictionary<string, TFrame> TagFramesDict { get; set; }

        /// <summary>
        /// 标题的快速访问方式
        /// </summary>
        public abstract string Title { get; set; }

        /// <summary>
        /// 艺术家的快速访问方式
        /// </summary>
        public abstract string Artist { get; set; }

        /// <summary>
        /// 专辑名称的快速访问方式
        /// </summary>
        public abstract string Album { get; set; }

        /// <summary>
        /// 年代的快速访问方式
        /// </summary>
        public abstract string Year { get; set; }

        /// <summary>
        /// 文件格式的快速访问方式
        /// </summary>
        public abstract string Format { get; set; }

        /// <summary>
        /// 获取/修改专辑图片
        /// </summary>
        public abstract Bitmap Picture { get; set; }

        public abstract byte[] ToBytes();
    }

}
