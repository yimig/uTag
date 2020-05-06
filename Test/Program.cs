using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uTag;
using uTag.Util;

namespace Test
{
    class Program
    {
        private const string en = @"H:\开发\样本\tag shooter\Jonas Brothers - Sucker.mp3";
        private const string cn = @"H:\开发\样本\tag shooter\何崇志 - Mozart Piano Sonata K.331 Andante Grazioso.mp3";
        private const string qq = @"H:\开发\样本\tag shooter\What Makes You Beautiful.mp3";
        private const string flac = @"H:\开发\样本\tag shooter\11 - Running out of Tomorrows.flac";


        static void Main(string[] args)
        {
            var tag = TagFactory.Load(flac);
            Console.WriteLine("class：" + tag.GetType().Name);
            Console.WriteLine("title：" + tag.Title);
            Console.WriteLine("artist：" + tag.Artist);
            Console.WriteLine("album：" + tag.Album);
            Console.WriteLine("year:" + tag.Year);
            Console.WriteLine("format：" + tag.Format);
            Console.ReadLine();
        }
    }
}