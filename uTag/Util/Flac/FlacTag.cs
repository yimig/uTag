using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace uTag.Util.Flac
{
    public class FlacTag:Tag<FlacTagHeader,FlacTagFrame>
    {
        public FlacTag(FileInfo file)
        {
            TagFramesDict = new Dictionary<string,FlacTagFrame>();
            BlocksList=new List<FlacBlock>();
            PictureList=new List<FlacTagPicture>();
            BinaryReader br = new BinaryReader(new FileStream(file.FullName, FileMode.Open));
            var rawTagHeader = br.ReadBytes(42);
            TagHeader = new FlacTagHeader(rawTagHeader);
            if(!TagHeader.BlockHeader.IsLastMetaData)InitBlocks(br);
            initFramesDict(br.ReadBytes(TagHeader.Size));
            InitPictures();
        }

        public void InitBlocks(BinaryReader br)
        {
            for (;;)
            {
                FlacTagBlockHeader blockHeader = new FlacTagBlockHeader(br.ReadBytes(4));
                byte[] blockContentBytes = br.ReadBytes(blockHeader.BlockSize);
                BlocksList.Add(new FlacBlock(blockContentBytes, blockHeader));
                if (blockHeader.IsLastMetaData) break;
            }
        }

        public void InitPictures()
        {
            var rawPicList = BlocksList.FindAll(b => b.BlockHeader.Type == FlacTagBlockHeader.BlockType.PICTURE);
            foreach (var rawPictureBlock in rawPicList)
            {
                PictureList.Add(new FlacTagPicture(rawPictureBlock.ToBytes()));
            }
        }

        public override List<FlacTagFrame> TagFramesFactory(byte[] rawFramesBytes)
        {
            List<FlacTagFrame>tempTagFrames= new List<FlacTagFrame>();
            if (BlocksList.Any(b => b.BlockHeader.Type == FlacTagBlockHeader.BlockType.VORBIS_COMMEN))
            {
                var metaBlock = BlocksList.Find(b => b.BlockHeader.Type == FlacTagBlockHeader.BlockType.VORBIS_COMMEN);
                var metaString=metaBlock.Content;
                var metaStrArray = metaString.Split(new string[]{"\0\0\0","\0\0"},StringSplitOptions.RemoveEmptyEntries);
                for(int i=0;i<metaStrArray.Length;i++)
                {
                    if (metaStrArray[i].Length > 1) metaStrArray[i]=metaStrArray[i].Remove(metaStrArray[i].Length - 1);
                    if(!metaStrArray[i].Contains("="))continue;
                    tempTagFrames.Add(new FlacTagFrame(metaStrArray[i]));
                }
            }

            return tempTagFrames;
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }

        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        public override string Title
        {
            get => this["TITLE"];
            set
            {

            }
        }
        public override string Artist
        {
            get => this["ARTIST"];
            set
            {

            }
        }
        public override string Album
        {
            get => this["ALBUM"];
            set
            {

            }
        }
        public override string Year
        {
            get => this["DATE"];
            set
            {

            }
        }
        public override string Format => "Flac";

        public override byte[] Picture
        {
            get => PictureList[0].Content;
            set
            {

            }
        }

        public override string TrackNumber
        {
            get => this["TRACKNUMBER"];
            set
            {

            }
        }
        public override string Genre
        {
            get => this["GENRE"];
            set
            {

            }       
        }
        public List<FlacBlock> BlocksList { get; set; }
        public List<FlacTagPicture> PictureList { get; set; }
    }
}
