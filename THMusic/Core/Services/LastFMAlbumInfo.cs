//***************************************************************************************************
//Name of File:     LastFMAlbumInfo.cs
//Description:      The LastFMAlbumInfo class describes the information retrieved from calls to  LastFM.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Collections.Generic;

namespace Core.Services
{
    //  TODO: Refactor this and complete the descriptions once the service is properly implemented.

    /// <summary>
    /// This <c>LastFMAlbumInfo</c> class describes the information retrieved from 
    /// the LastFM web service for various calls to its methods.
    /// </summary>
    public class LastFMAlbumInfo
    {
        /// <summary>
        /// This <c>LastFMAlbumInfo</c> class describes the return
        /// from the LastFM.Album.GetInfo method call.
        /// </summary>
        public LastFMAlbumInfo()
        {
            this.name = string.Empty;
            this.artist = new LastFMArtist();
            this.id = string.Empty;
            this.mbid = string.Empty;
            this.url = string.Empty;
            this.releasedDate = string.Empty;
            this.images = new List<LastFMArtwork>();
            this.listeners = string.Empty;
            this.playcount = string.Empty;
            this.tracks = new List<LastFMTrack>();
            this.tags = new List<LastFMTag>();
            this.wiki = new LastFMWiki();
            this.Status = new LastFMError();
        }

        //  Album information retrieved from LastFM
        public string name { get; set; }
        public LastFMArtist artist { get; set; }
        public string id { get; set; }
        public string mbid { get; set; }
        public string url { get; set; }
        public string releasedDate { get; set; }
        public IList<LastFMArtwork> images { get; set; }
        public string listeners { get; set; }
        public string playcount { get; set; }
        public ICollection<LastFMTrack> tracks { get; set; }
        public ICollection<LastFMTag> tags { get; set; }
        public LastFMWiki wiki { get; set; }
        public LastFMError Status { get; set; }
    }


    public class LastFMTrack
    {
        public LastFMTrack()
        {
            this.rank = string.Empty;
            this.name = string.Empty;
            this.duration = string.Empty;
            this.mbid = string.Empty;
            this.url = string.Empty;
            this.streamable = string.Empty;
            this.streamFullTrack = string.Empty;
            this.artist = new LastFMArtist();
        }

        public string rank { get; set; }
        public string name { get; set; }
        public string duration { get; set; }
        public string mbid { get; set; }
        public string url { get; set; }
        public string streamable { get; set; }
        public string streamFullTrack { get; set; }
        public LastFMArtist artist { get; set; }

    }

    public class LastFMArtist
    {
        public LastFMArtist()
        {
            this.name = string.Empty;
            this.url = string.Empty;
            this.mbid = string.Empty;
        }

        public string name { get; set; }
        public string url { get; set; }
        public string mbid { get; set; }
    }

    public class LastFMArtwork
    {
        public LastFMArtwork()
        {
            this.size = string.Empty;
            this.imageUrl = string.Empty;
        }

        public string size { get; set; }
        public string imageUrl { get; set; }
    }

    public class LastFMTag
    {
        public LastFMTag()
        {
            this.name = string.Empty;
            this.url = string.Empty;
        }

        public string name { get; set; }
        public string url { get; set; }
    }


    public class LastFMWiki
    {
        public LastFMWiki()
        {
            this.summary = string.Empty;
            this.content = string.Empty;
            this.published = string.Empty;
        }

        public string published { get; set; }
        public string summary {get; set;}
        public string content { get; set; }
    }



    public class LastFMError
    {
        public LastFMError()
        {
            this.code = string.Empty;
            this.message = string.Empty;
        }

        public string code { get; set; }
        public string message { get; set; }
    }
}
