using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace uTag
{
    /// <summary>
    /// 标签段接口，是标签都得实现
    /// </summary>
    public interface ITagSection
    {
        /// <summary>
        /// 将目前的内容以比特串方式返回
        /// </summary>
        /// <returns></returns>
        byte[] ToBytes();
    }

    /// <summary>
    /// 标签帧接口
    /// </summary>
    public interface ITagFrame:ITagSection
    {
        /// <summary>
        /// 以字符串的形式输出标签帧内容
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// 帧ID
        /// </summary>
        string Id { get; set; }
    }

    /// <summary>
    /// 标签头接口
    /// </summary>
    public interface ITagHeader:ITagSection
    {
        /// <summary>
        /// 标签协议版本
        /// </summary>
        string Version { get; }

        /// <summary>
        /// 标签总大小
        /// </summary>
        int Size { get; set; }
    }

    /// <summary>
    /// 标签接口，定义常用的标签快速访问方式，忽视各标签协议的差异，做到无差别读取
    /// </summary>
    public interface ITag : ITagSection
    {
        /// <summary>
        /// 标题的快速访问方式
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// 艺术家的快速访问方式
        /// </summary>
        string Artist { get; set; }

        /// <summary>
        /// 专辑名称的快速访问方式
        /// </summary>
        string Album { get; set; }

        /// <summary>
        /// 年代的快速访问方式
        /// </summary>
        string Year { get; set; }

        /// <summary>
        /// 返回标签格式
        /// </summary>
        string Format { get; }

        /// <summary>
        /// 获取/修改专辑图片
        /// </summary>
        Bitmap Picture { get; set; }
    }

    /// <summary>
    /// 标签类，可派生出各个标准的标签
    /// </summary>
    /// <typeparam name="THeader">标签头</typeparam>
    /// <typeparam name="TFrame">标签帧</typeparam>
    public abstract class Tag<THeader, TFrame>:ITag
        where THeader : ITagHeader
        where TFrame : ITagFrame
    {

        /// <summary>
        /// 如果想要单独得到标签帧，可使用索引器搜索标签帧关键字得到
        /// </summary>
        /// <param name="tagKey">标签帧关键字</param>
        /// <returns>字符串内容，若不能转换为字符串则报错TagNotStringException</returns>
        public string this[string tagKey]
        {
            get => TagFramesDict[tagKey].Content;
            set => TagFramesDict[tagKey].Content=value;
        }

        /// <summary>
        /// 生产分割标签帧方法
        /// </summary>
        /// <param name="rawFramesBytes">原标签帧集合（不包含标签头）</param>
        /// <returns>标签帧集合</returns>
        public abstract List<TFrame> TagFramesFactory(byte[] rawFramesBytes);

        /// <summary>
        /// 初始化标签帧字典
        /// </summary>
        /// <param name="rawFramesBytes">原标签帧集合（不包含标签头）</param>
        public void initFramesDict(byte[] rawFramesBytes)
        {
            var framesList = TagFramesFactory(rawFramesBytes);
            foreach (var frame in framesList)
            {
                if (TagFramesDict.Keys.Contains(frame.Id))
                {
                    continue;
                }
                TagFramesDict.Add(frame.Id,frame);
            }
        }

        /// <summary>
        /// 标签头信息
        /// </summary>
        public THeader TagHeader { get; set; }

        /// <summary>
        /// 标签帧集合
        /// </summary>
        public Dictionary<string, TFrame> TagFramesDict { get; set; }

        public abstract byte[] ToBytes();

        public abstract string Title { get; set; }
        public abstract string Artist { get; set; }
        public abstract string Album { get; set; }
        public abstract string Year { get; set; }
        public abstract string Format { get; }
        public abstract Bitmap Picture { get; set; }
    }

}