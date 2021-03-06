﻿using System;
using System.Collections.Generic;
using System.Text;

namespace uTag
{
    /// <summary>
    /// 编辑标签时发生异常
    /// </summary>
    public abstract class TagException : Exception
    {
        public abstract string GetExplanation();
    }

    /// <summary>
    /// 某些标签包含的内容不能转换为字符串，比如图片，强行将其转换为字符串时将报这个错误
    /// </summary>
    public class TagNotStringException : TagException
    {
        public override string GetExplanation()
        {
            return "某些标签包含的内容不能转换为字符串，比如图片，强行将其转换为字符串时将报这个错误";
        }
    }

    /// <summary>
    /// 该格式无法自动识别或不受支持，请手动指定标签格式
    /// </summary>
    public class CannotDetectTagFormatException : TagException
    {
        public override string GetExplanation()
        {
            return "该格式无法自动识别或不受支持，请手动指定标签格式";
        }
    }

    /// <summary>
    /// 分析文件错误，文件损坏或格式不支持
    /// </summary>
    public class TagAnalyseException : TagException
    {
        public override string GetExplanation()
        {
            return "分析文件错误，文件损坏或格式不支持";
        }
    }

    /// <summary>
    /// 图片存在，但图片损坏或不为jpeg格式
    /// </summary>
    public class TagPictureBadException:TagException
    {
        public override string GetExplanation()
        {
            return " 图片存在，但图片损坏或不为jpeg格式";
        }
    }

    /// <summary>
    /// 写入标签时发生的错误
    /// </summary>
    public abstract class TagWriteException : TagException
    {

    }

    /// <summary>
    /// 无法写入标签信息：标签大小超过标准限制
    /// </summary>
    public class TagOverSizeException : TagWriteException
    {
        public override string GetExplanation()
        {
            return "无法写入标签信息：标签大小超过标准限制";
        }
    }
}