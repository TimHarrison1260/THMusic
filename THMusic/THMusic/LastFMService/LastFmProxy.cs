//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using System.Xml.Linq;              //  Linq to XML
//using System.Net;                   //  Url Encoding stuff
////using System.Runtime.Serialization;
////using System.ServiceModel.Web;

//using System.Net.Http;
//using Newtonsoft.Json;              //  Json.Net;

//namespace TagLibExample.LastFMService
//{
//    public class LastFmProxy
//    {
//        public static async Task<LastFMAlbumInfo> GetAlbumInfo(string albumName, string artistName)
//        {
//            var info = new LastFMAlbumInfo();

//            //HttpClient client = new HttpClient();
//            //string url = "http://yourservice.com/some/resource";
//            //HttpResponseMessage response = await client.GetAsync(url);
//            //return response.Content.ReadAsString();

//            //  Encode the parameters before including in the url
//            //  Do NOT encode the full url as this causes problems and corrupts the url.
//            string encodedAlbumName = WebUtility.UrlEncode(albumName);
//            string encodedArtistName = WebUtility.UrlEncode(artistName);

//            //  create the Url for the Google Elevation Api
//            string url = string.Format("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=883f78cd9f3317abb2e2a88008dc0e99&artist={0}&album={1}", encodedArtistName, encodedAlbumName);
//            //string url = string.Format("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=883f78cd9f3317abb2e2a88008dc0e99&artist={0}&album={1}&format=json");

//            //  Create the result by streaming the return from the load(url).  This has
//            //  the effect of submitting an HttpWebRequest as the "file" is a url which 
//            //  returns XML serialised data.
//            //  We could also have the results returned in JSON format, but we would need
//            //  a different parsers.  C# is easiest with XML at this level.

//            //await Task<string>(

//            //Action getXML = new 

////            var resultAsync = await Task.Run();
//            var result = XElement.Load(url);

//            //  Get the google statues returned
//            //var lfm = result.Element("lfm");
//            var status = (string)result.Attribute("status"); 


//            if (status == "ok")
//            {
//                //  Deserialize the result
//                //  Album specific informtaion
//                LastFMAlbumInfo album = new LastFMAlbumInfo();
//                var albumElement = result.Element("album");
//                album.name = (string)albumElement.Element("name");
//                album.artist = (string)albumElement.Element("artist");
//                album.id = (string)albumElement.Element("id");
//                album.mbid = (string)albumElement.Element("mbid");
//                album.url = (string)albumElement.Element("url");
//                album.releasedDate = (string)albumElement.Element("releasedate");

//                //  Artwork
//                List<LastFMArtwork> images = new List<LastFMArtwork>();
//                var imageElements = albumElement.Descendants("image").Select(i => i);
//                foreach (var i in imageElements)
//                {
//                    var image = new LastFMArtwork();
//                    image.size = (string)i.Attribute("size");
//                    image.imageUrl = (string)i;
//                    images.Add(image);
//                }
//                album.images = images;

//                //  Additional basic album information
//                album.listeners = (string)albumElement.Element("listeners");
//                album.playcount = (string)albumElement.Element("playcount");

//                //  Tracks
//                List<LastFMTrack> tracks = new List<LastFMTrack>();
//                var tracksElement = albumElement.Element("tracks");
//                var trackElements = tracksElement.Descendants("track").Select(t => t);
//                foreach (var t in trackElements)
//                {
//                    var track = new LastFMTrack();
//                    track.rank = (string)t.Attribute("rank");
//                    track.name =(string)t.Element("name");
//                    track.duration = (string)t.Element("duration");
//                    track.mbid = (string)t.Element("mbid");
//                    track.url = (string)t.Element("url");
//                    track.streamable = (string)t.Element("streamable");
//                    track.streamFullTrack = (string)t.Element("streamable").Attribute("fulltrack");

//                    var trackArtist = new LastFMArtist();
//                    var artistElement = t.Element("artist");
//                    trackArtist.name = (string)artistElement.Element("name");
//                    trackArtist.mbid = (string)artistElement.Element("mbid");
//                    trackArtist.url = (string)artistElement.Element("url");

//                    track.artist = trackArtist;

//                    tracks.Add(track);
//                }
//                album.tracks = tracks;

//                //  Tag information
//                List<LastFMTag> tags = new List<LastFMTag>();
//                var topTags = albumElement.Element("toptags");
//                var tagElements = topTags.Descendants("tag").Select(t => t);
//                foreach (var t in tagElements)
//                {
//                    var tag = new LastFMTag();
//                    tag.name = (string)t.Element("name");
//                    tag.url = (string)t.Element("url");
//                    tags.Add(tag);
//                }
//                album.tags = tags;

//                //  Wiki information
//                var wiki = new LastFMWiki();
//                var wikiElement = albumElement.Element("wiki");
//                wiki.published = (string)wikiElement.Element("published");
//                wiki.summary = (string)wikiElement.Element("summary");
//                wiki.content = (string)wikiElement.Element("content");
//                album.wiki = wiki;


//                return album;
//            }
//            else
//            {
//                //  Get the message from the returned string
//                LastFMError error = new LastFMError();
//                var errorElement = result.Element("error");
//                error.code = (string)errorElement.Attribute("code");
//                error.message = (string)errorElement;

//                throw new Exception(string.Format("There were problems getting the Album information from the LastFM\r\nReturn Code: {0}\r\nMEssage: {1}", error.code, error.message));
//            }
//        }


//        public static async Task<RootObject> GetAlbumInfoAsync(string AlbumName, string ArtistName)
//        {
//            RootObject resultItem = new RootObject();

//            HttpClient client = new HttpClient();

//            //  Encode the parameters before including in the url
//            //  Do NOT encode the full url as this causes problems and corrupts the url.
//            string encodedAlbumName = WebUtility.UrlEncode(AlbumName);
//            string encodedArtistName = WebUtility.UrlEncode(ArtistName);

//            //  create the Url for the Google Elevation Api
//            //string url = string.Format("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=883f78cd9f3317abb2e2a88008dc0e99&artist={0}&album={1}", encodedArtistName, encodedAlbumName);
//            string url = string.Format("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=883f78cd9f3317abb2e2a88008dc0e99&artist={0}&album={1}&format=json", encodedArtistName, encodedAlbumName);

//            HttpResponseMessage response = await client.GetAsync(url);
//            if (response.StatusCode == HttpStatusCode.OK)
//            {
//                string responseString = await response.Content.ReadAsStringAsync();
//                // parse to json
//                resultItem = JsonConvert.DeserializeObject<RootObject>(responseString);
//            }
//            else
//            {
//                throw new Exception(response.StatusCode.ToString() + " " + response.ReasonPhrase);
//            }

//            return resultItem;






//        }

//    }
//}
