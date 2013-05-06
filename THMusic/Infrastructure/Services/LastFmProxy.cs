//***************************************************************************************************
//Name of File:     LastFMProxy.cs
//Description:      The provides the LastFM functionality from the serivce, abstracting it away from the 
//                  resst of the applicatinon.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;              //  Linq to XML
using System.Net;                   //  Url Encoding stuff
//using System.Runtime.Serialization;
//using System.ServiceModel.Web;

//using System.Net.Http;
//using Newtonsoft.Json;              //  Json.Net;

using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

using Core.Services;
using System.IO;

namespace Infrastructure.Services
{
    /// <summary>
    /// This <c>LastFMProxh</c> class acts as a proxy to the LastFM http service, that
    /// supplies information about music albums, artists and just about everything 
    /// else associated with music.
    /// </summary>
    public class LastFmProxy : ILastFMService
    {
        /// <summary>
        /// </summary>
        /// <param name="albumName"></param>
        /// <param name="artistName"></param>
        /// <returns></returns>
        /// <remarks>
        /// This methods contains no asynchronous call, and is therefore the bottleneck
        /// in the asynchronous nature of the application.  This is due to XElement not
        /// providing an asynchronous method to call it.
        /// However, by making this method itself callabe in an asynchronous way, allows
        /// the UI to remain unblocked.
        /// Using XElement to call the LastFM XML restful service rather than the Json version
        /// because the Json deserialiser in currently not providing a fully working model,
        /// it currently contain some invalid names properties.  Tried using NewtonSoft.Json
        /// which is a .Net implementation of a Json deserialiser, but it does not fully
        /// retrieve the model as yet.
        /// Don't want to spend too long learning how to configure this deserialiser just
        /// now as there's not much time for this project.  However, if it can be configured 
        /// to retrieve the full model, then it would be possible to make the call to the
        /// LastFM service asynchronously, which ultimately would be the best way.
        /// </remarks>
        public async Task<LastFMAlbumInfo> GetAlbumInfoAsync(string albumName, string artistName)
        {
            var info = new LastFMAlbumInfo();           //  This should created using a Factory!!!

            //  Encode the parameters before including in the url
            //  Do NOT encode the full url as this causes problems and corrupts the url.
            string encodedAlbumName = WebUtility.UrlEncode(albumName);
            string encodedArtistName = WebUtility.UrlEncode(artistName);

            //  create the Url for the Google Elevation Api
            string url = string.Format("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=883f78cd9f3317abb2e2a88008dc0e99&artist={0}&album={1}", encodedArtistName, encodedAlbumName);
            //string url = string.Format("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=883f78cd9f3317abb2e2a88008dc0e99&artist={0}&album={1}&format=json");

            //  Create the result by streaming the return from the load(url).  This has
            //  the effect of submitting an HttpWebRequest as the "file" is a url which 
            //  returns XML serialised data.
            //  We could also have the results returned in JSON format, but we would need
            //  a different parsers.  C# is easiest with XML at this level.
            var result = XElement.Load(url);

            //  TODO: Refactor this into more specific methods, and keep this one to combind the individual methods.

            //  Get the google statues returned
            var status = (string)result.Attribute("status");
            if (status == "ok")
            {
                //  Deserialize the result
                //  Album specific informtaion
                LastFMAlbumInfo album = new LastFMAlbumInfo();
                var albumElement = result.Element("album");
                album.name = (string)albumElement.Element("name");

                album.id = (string)albumElement.Element("id");
                album.mbid = (string)albumElement.Element("mbid");
                album.url = (string)albumElement.Element("url");
                album.releasedDate = (string)albumElement.Element("releasedate");

                // Artist stuff.  Only name here, tracks hold url etc.
                album.artist = new LastFMArtist();
                album.artist.name = (string)albumElement.Element("artist");

                //  Artwork
                List<LastFMArtwork> images = new List<LastFMArtwork>();
                var imageElements = albumElement.Descendants("image").Select(i => i);
                if (imageElements != null)
                {
                    foreach (var i in imageElements)
                    {
                        var image = new LastFMArtwork();
                        image.size = (string)i.Attribute("size");
                        image.imageUrl = (string)i;
                        images.Add(image);
                    }
                }
                album.images = images;

                //  Additional basic album information
                album.listeners = (string)albumElement.Element("listeners");
                album.playcount = (string)albumElement.Element("playcount");

                //  Tracks
                List<LastFMTrack> tracks = new List<LastFMTrack>();
                var tracksElement = albumElement.Element("tracks");
                if (tracksElement != null)
                {
                    var trackElements = tracksElement.Descendants("track").Select(t => t);
                    foreach (var t in trackElements)
                    {
                        var track = new LastFMTrack();
                        track.rank = (string)t.Attribute("rank");
                        track.name = (string)t.Element("name");
                        track.duration = (string)t.Element("duration");
                        track.mbid = (string)t.Element("mbid");
                        track.url = (string)t.Element("url");
                        track.streamable = (string)t.Element("streamable");
                        track.streamFullTrack = (string)t.Element("streamable").Attribute("fulltrack");

                        var trackArtist = new LastFMArtist();
                        var artistElement = t.Element("artist");
                        trackArtist.name = (string)artistElement.Element("name");
                        trackArtist.mbid = (string)artistElement.Element("mbid");
                        trackArtist.url = (string)artistElement.Element("url");

                        track.artist = trackArtist;

                        tracks.Add(track);

                        //  Add the artist for the Track to the Album, the album only has the artist name.
                        if (album.artist.mbid == null || album.artist.mbid == string.Empty)
                            album.artist.mbid = trackArtist.mbid;
                        if (album.artist.url == null || album.artist.url == string.Empty)
                            album.artist.url = trackArtist.url;
                    }
                }
                album.tracks = tracks;

                //  Tag information
                List<LastFMTag> tags = new List<LastFMTag>();
                var topTags = albumElement.Element("toptags");
                if (topTags != null)
                {
                    var tagElements = topTags.Descendants("tag").Select(t => t);
                    foreach (var t in tagElements)
                    {
                        var tag = new LastFMTag();
                        tag.name = (string)t.Element("name");
                        tag.url = (string)t.Element("url");
                        tags.Add(tag);
                    }
                }
                album.tags = tags;

                //  Wiki information
                var wiki = new LastFMWiki();
                var wikiElement = albumElement.Element("wiki");
                if (wikiElement != null)
                {
                    wiki.published = (string)wikiElement.Element("published");
                    wiki.summary = (string)wikiElement.Element("summary");
                    wiki.content = (string)wikiElement.Element("content");
                }
                album.wiki = wiki;


                return album;
            }
            else
            {
                //  Get the message from the returned string
                LastFMError error = new LastFMError();
                var errorElement = result.Element("error");
                error.code = (string)errorElement.Attribute("code");
                error.message = (string)errorElement;

                throw new Exception(string.Format("There were problems getting the Album information from the LastFM\r\nReturn Code: {0}\r\nMEssage: {1}", error.code, error.message));
            }
        }

    }
}
