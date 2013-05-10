//***************************************************************************************************
//Name of File:     LastFMHelper.cs
//Description:      The provides supporting functionality to LastFMProxy to load retrieved data.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Core.Services;

namespace Infrastructure.Services
{
    /// <summary>
    /// This static <c>LastFMHelper</c> class provides supporting functionality to
    /// the <see cref="Infrastructure.Services.LastFMProxy"/> class.  It provides
    /// methods to load the Track, Images, Genres and Wiki information from the 
    /// retrieved XML data from the call to LastFM Album.GetInfo service.
    /// </summary>
    public static class LastFMHelper
    {
        /// <summary>
        /// Deserialises the XML response from the LastFM HTTP Request
        /// </summary>
        /// <param name="resultElement">The XML returned from the LastFM call</param>
        /// <returns>Deserialised to an instance of LastFMAlbumInfo</returns>
        public static LastFMAlbumInfo Deserialise(XElement result)
        {
            //  Album specific informtaion * Artist Name
            var albumElement = result.Element("album");
            var album = LoadAlbumInfo(albumElement);

            //  Artwork
            album.images = LastFMHelper.LoadImageInfo(albumElement);

            //  Tracks: Not totally abstracted as the Artist is updated from track information
            List<LastFMTrack> tracks = new List<LastFMTrack>();
            var tracksElement = albumElement.Element("tracks");
            if (tracksElement != null)
            {
                var trackElements = tracksElement.Descendants("track").Select(t => t);
                foreach (var t in trackElements)
                {
                    var track = LoadTrackInfo(t);
                    tracks.Add(track);

                    //  Add the artist for the Track to the Album, the album only has the artist name.
                    if (album.artist.mbid == null || album.artist.mbid == string.Empty)
                        album.artist.mbid = track.artist.mbid;
                    if (album.artist.url == null || album.artist.url == string.Empty)
                        album.artist.url = track.artist.url;
                }
            }
            album.tracks = tracks;

            //  Tag information
            album.tags = LoadTagInfo(albumElement);

            //  Wiki information
            album.wiki = LoadWikiInfo(albumElement);

            return album;
        }



        /// <summary>
        /// Retrieves the basic album information from the supplied XML Xelement
        /// are returns an instance of the LastFMAlbumInfo class
        /// </summary>
        /// <param name="albumElement">The Album XElement</param>
        /// <returns>A LastFMAlbumInfo class</returns>
        private static LastFMAlbumInfo LoadAlbumInfo(XElement albumElement)
        {
            LastFMAlbumInfo album = new LastFMAlbumInfo();

            album.name = (string)albumElement.Element("name");

            album.id = (string)albumElement.Element("id");
            album.mbid = (string)albumElement.Element("mbid");
            album.url = (string)albumElement.Element("url");
            album.releasedDate = (string)albumElement.Element("releasedate");

            // Artist stuff.  Only name here, tracks hold url etc.
            album.artist = new LastFMArtist();
            album.artist.name = (string)albumElement.Element("artist");

            album.listeners = (string)albumElement.Element("listeners");
            album.playcount = (string)albumElement.Element("playcount");

            return album;
        }

        /// <summary>
        /// Retrieves the track information from the supplied XML element
        /// and loads an instance of the LastFMTrack class
        /// </summary>
        /// <param name="albumInfo">The Track XElement</param>
        /// <returns>A LastFMTrack</returns>
        private static LastFMTrack LoadTrackInfo(XElement TrackElement)
        {
            var track = new LastFMTrack();

            track.rank = (string)TrackElement.Attribute("rank");
            track.name = (string)TrackElement.Element("name");
            track.duration = (string)TrackElement.Element("duration");
            track.mbid = (string)TrackElement.Element("mbid");
            track.url = (string)TrackElement.Element("url");
            track.streamable = (string)TrackElement.Element("streamable");
            track.streamFullTrack = (string)TrackElement.Element("streamable").Attribute("fulltrack");

            var trackArtist = new LastFMArtist();
            var artistElement = TrackElement.Element("artist");
            trackArtist.name = (string)artistElement.Element("name");
            trackArtist.mbid = (string)artistElement.Element("mbid");
            trackArtist.url = (string)artistElement.Element("url");

            track.artist = trackArtist;

            return track;
        }

        /// <summary>
        /// Retrieves the track inforamtion from the suplied XML Element
        /// and loads a list of LastFMArtwork classes
        /// </summary>
        /// <param name="albumElement">The Album XElement</param>
        /// <returns>A list of LastFMArtwork classes</returns>
        private static List<LastFMArtwork> LoadImageInfo(XElement albumElement)
        {
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
            return images;
        }

        /// <summary>
        /// Retrieves the Tag information from the supplied XML Element
        /// and loads a list of LastFMTag classes
        /// </summary>
        /// <param name="albumElement">The Album XElement</param>
        /// <returns>The list of LastFMTag classes</returns>
        private static List<LastFMTag> LoadTagInfo(XElement albumElement)
        {
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
            return tags;
        }

        /// <summary>
        /// Retrieves the Wiki information from the supplied XML Element
        /// and loads a LastFMWiki class
        /// </summary>
        /// <param name="albumElement">The Album XElement</param>
        /// <returns>A LastFMWiki class</returns>
        private static LastFMWiki LoadWikiInfo(XElement albumElement)
        {
            var wiki = new LastFMWiki();
            var wikiElement = albumElement.Element("wiki");
            if (wikiElement != null)
            {
                wiki.published = (string)wikiElement.Element("published");
                wiki.summary = (string)wikiElement.Element("summary");
                wiki.content = (string)wikiElement.Element("content");
            }
            return wiki;
        }
    }
}
