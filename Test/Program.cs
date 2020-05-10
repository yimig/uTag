using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uTag;
using uTag.Util;
using System.Drawing;

namespace Test
{
    class Program
    {
        private const string en = @"H:\开发\样本\tag shooter\Jonas Brothers - Sucker.mp3";
        private const string cn = @"H:\开发\样本\tag shooter\何崇志 - Mozart Piano Sonata K.331 Andante Grazioso.mp3";
        private const string qq = @"H:\开发\样本\tag shooter\What Makes You Beautiful.mp3";
        private const string flac = @"H:\开发\样本\tag shooter\Traumerei.flac";
        private const string fen = @"H:\开发\样本\tag shooter\13.b小调前奏曲.flac";
        private const string fjp = @"H:\开发\样本\tag shooter\茜さす (Tv Size).flac";


        static void Main(string[] args)
        {
            var tag = TagFactory.Load(fjp);
            Console.WriteLine("class：" + tag.GetType().Name);
            Console.WriteLine("title：" + tag.Title);
            Console.WriteLine("artist：" + tag.Artist);
            Console.WriteLine("album：" + tag.Album);
            Console.WriteLine("year:" + tag.Year);
            Console.WriteLine("format：" + tag.Format);
            Console.WriteLine("TrackID："+tag.TrackNumber);
            Console.WriteLine("Genre："+tag.Genre);
            MemoryStream ms=new MemoryStream(tag.Picture);
            Image image=Image.FromStream(ms);
            Console.WriteLine("Picture Area："+image.Height+"*"+image.Width);
            Console.ReadLine();
        }
    }
}