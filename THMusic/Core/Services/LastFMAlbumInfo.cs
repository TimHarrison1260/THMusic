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
        /// <summary>
        /// Gets or sets the name of the album
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Gets of sets the Artist of the album
        /// </summary>
        public LastFMArtist artist { get; set; }
        /// <summary>
        /// Gets or sets the id of the album
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Gets or sets the MusicBrainzId of the album
        /// </summary>
        public string mbid { get; set; }
        /// <summary>
        /// Gets or sets the LastFM url for the online album information
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// Gets or sets the release date of the album
        /// </summary>
        public string releasedDate { get; set; }
        /// <summary>
        /// Gets or sets the Image urls for the album
        /// </summary>
        public IList<LastFMArtwork> images { get; set; }
        /// <summary>
        /// Gets or sets the number of listeners of the album
        /// </summary>
        public string listeners { get; set; }
        /// <summary>
        /// Gets or sets the playcount of the album
        /// </summary>
        public string playcount { get; set; }
        /// <summary>
        /// Gets or sets the collection of tracks for the album
        /// </summary>
        public ICollection<LastFMTrack> tracks { get; set; }
        /// <summary>
        /// Gets or sets the collection of tags for the album
        /// </summary>
        public ICollection<LastFMTag> tags { get; set; }
        /// <summary>
        /// Gets or sets the Wiki information for the album
        /// </summary>
        public LastFMWiki wiki { get; set; }
        /// <summary>
        /// Gets or sets the Status of the album
        /// </summary>
        public LastFMError Status { get; set; }
    }


    /// <summary>
    /// This <c>LastFMTrack</c> class describes the information retrieved from 
    /// the LastFM web service for various calls to its methods.
    /// </summary>
    public class LastFMTrack
    {
        /// <summary>
        /// Constructor to initialise the class
        /// </summary>
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

        /// <summary>
        /// Gets or sets the Rank, track number
        /// </summary>
        public string rank { get; set; }
        /// <summary>
        /// Gets or sets the Track name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the track duration
        /// </summary>
        public string duration { get; set; }
        /// <summary>
        /// Gets or sets the MusicBrainzId for the track
        /// </summary>
        public string mbid { get; set; }
        /// <summary>
        /// Gets or sets the Url for the track
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// Gets or sets whether the tack is streamable
        /// </summary>
        public string streamable { get; set; }
        /// <summary>
        /// Gets or sets whether the complete track can be streamed
        /// </summary>
        public string streamFullTrack { get; set; }
        /// <summary>
        /// Gets or sets the Artist associated with the track
        /// </summary>
        public LastFMArtist artist { get; set; }

    }

    /// <summary>
    /// This <c>LastFMArtist</c> class describes the information retrieved from 
    /// the LastFM web service for various calls to its methods.
    /// </summary>
    public class LastFMArtist
    {
        /// <summary>
        /// Constructor to initialise the class
        /// </summary>
        public LastFMArtist()
        {
            this.name = string.Empty;
            this.url = string.Empty;
            this.mbid = string.Empty;
        }

        /// <summary>
        /// Gets or sets the Artist name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the Artist LAstFM Url
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// Gets or sets the MusicBrainzId for the Artist
        /// </summary>
        public string mbid { get; set; }
    }

    /// <summary>
    /// This <c>LastFMArtwork</c> class describes the information retrieved from 
    /// the LastFM web service for various calls to its methods.
    /// </summary>
    public class LastFMArtwork
    {
        /// <summary>
        /// Constructor to initialise the class
        /// </summary>
        public LastFMArtwork()
        {
            this.size = string.Empty;
            this.imageUrl = string.Empty;
        }

        /// <summary>
        /// Gets or sets the size of the image
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// Gets or sets the LastFM url to the image
        /// </summary>
        public string imageUrl { get; set; }
    }

    /// <summary>
    /// This <c>LastFMTag</c> class describes the information retrieved from 
    /// the LastFM web service for various calls to its methods.
    /// </summary>
    public class LastFMTag
    {
        /// <summary>
        /// Constructor to initialise the class
        /// </summary>
        public LastFMTag()
        {
            this.name = string.Empty;
            this.url = string.Empty;
        }

        /// <summary>
        /// Gets or sets the name of the tag
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the LastFM url to the tag
        /// </summary>
        public string url { get; set; }
    }


    /// <summary>
    /// This <c>LastFMWiki</c> class describes the information retrieved from 
    /// the LastFM web service for various calls to its methods.
    /// </summary>
    public class LastFMWiki
    {
        /// <summary>
        /// Constructor to initialise the class
        /// </summary>
        public LastFMWiki()
        {
            this.summary = string.Empty;
            this.content = string.Empty;
            this.published = string.Empty;
        }

        /// <summary>
        /// Gets or sets the date the Wiki was published
        /// </summary>
        public string published { get; set; }
        /// <summary>
        /// Gets or sets a summary of the Wiki Content
        /// </summary>
        public string summary {get; set;}
        /// <summary>
        /// Gets or sets the full Wiki content
        /// </summary>
        public string content { get; set; }
    }



    /// <summary>
    /// This <c>LastFMError</c> class describes the information retrieved from 
    /// the LastFM web service for various calls to its methods.
    /// </summary>
    public class LastFMError
    {
        /// <summary>
        /// constructor
        /// </summary>
        public LastFMError()
        {
            this.code = string.Empty;
            this.message = string.Empty;
        }

        /// <summary>
        /// Gets or sets the code returned form the LastFM call
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// Gets or sets the corresponding message
        /// </summary>
        public string message { get; set; }
    }
}
