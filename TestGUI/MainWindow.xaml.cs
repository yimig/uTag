﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        private const string flac = @"H:\开发\样本\tag shooter\11 - Running out of Tomorrows.flac";

        public MainWindow()
        {
            InitializeComponent();
            tag = TagFactory.Load(en);
            InitControl();
        }

        private void InitControl()
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
