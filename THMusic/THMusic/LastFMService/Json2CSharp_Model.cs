//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TagLibExample.LastFMService
//{

//    public class Image
//    {
//        public string image { get; set; }       // imageUrl in XML.
//        public string size { get; set; }
//    }

//    public class Streamable
//    {
//        public string streamable { get; set; }
//        public string fulltrack { get; set; }
//    }

//    public class Artist
//    {
//        public string name { get; set; }
//        public string mbid { get; set; }
//        public string url { get; set; }
//    }

//    public class Attr
//    {
//        public string rank { get; set; }
//    }

//    public class Track
//    {
//        public string name { get; set; }
//        public string duration { get; set; }
//        public string mbid { get; set; }
//        public string url { get; set; }
//        public Streamable streamable { get; set; }
//        //public string streamable { get; set; }
//        //public string fulltrack { get; set; }
//        public Artist artist { get; set; }
//        public Attr rank { get; set; }
//    }

//    public class Tracks
//    {
//        public List<Track> track { get; set; }
//    }

//    public class Tag
//    {
//        public string name { get; set; }
//        public string url { get; set; }
//    }

//    public class Toptags
//    {
//        public List<Tag> tag { get; set; }
//    }

//    public class Wiki
//    {
//        public string published { get; set; }
//        public string summary { get; set; }
//        public string content { get; set; }
//    }

//    public class Album
//    {
//        public string name { get; set; }
//        public string artist { get; set; }
//        public string id { get; set; }
//        public string mbid { get; set; }
//        public string url { get; set; }
//        public string releasedate { get; set; }
//        public List<Image> image { get; set; }
//        public string listeners { get; set; }
//        public string playcount { get; set; }
//        public Tracks tracks { get; set; }
//        public Toptags toptags { get; set; }
//        public Wiki wiki { get; set; }
//    }

//    public class RootObject
//    {
//        public Album album { get; set; }
//    }



//}
