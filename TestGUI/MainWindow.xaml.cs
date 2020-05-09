using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using uTag;
using uTag.Util;

namespace TestGUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ITag tag;

        private const string en = @"H:\开发\样本\tag shooter\Jonas Brothers - Sucker.mp3";
        private const string cn = @"H:\开发\样本\tag shooter\何崇志 - Mozart Piano Sonata K.331 Andante Grazioso.mp3";
        private const string qq = @"H:\开发\样本\tag shooter\What Makes You Beautiful.mp3";
        private const string flac = @"H:\开发\样本\tag shooter\Traumerei.flac";
        private const string fen = @"H:\开发\样本\tag shooter\13.b小调前奏曲.flac";
        private const string fjp = @"H:\开发\样本\tag shooter\茜さす (Tv Size).flac";

        public MainWindow()
        {
            InitializeComponent();
            tag = TagFactory.Load(qq);
            InitControler();
        }

        private void InitControler()
        {
            this.lbTitle.Content = tag.Title;
            this.lbAlbum.Content = tag.Album;
            this.lbArtist.Content = tag.Artist;
            this.lbYEAR.Content = tag.Year;
            this.lbTagFormat.Content = tag.Format;
            this.lbTrack.Content = tag.TrackNumber;
            this.lbGenre.Content = tag.Genre;
            MemoryStream ms = new MemoryStream(tag.Picture);
            ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
            this.imageBox.Source = (ImageSource) imageSourceConverter.ConvertFrom(ms);
        }
    }
}
