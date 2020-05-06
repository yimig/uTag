using System;
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
    /// 分析文件错误，文件损坏或格式不支持
    /// </summary>
    public class TagAnalyseException : TagException
    {
        public override string GetExplanation()
        {
            return "分析文件错误，文件损坏或格式不支持";
        }
    }
}
