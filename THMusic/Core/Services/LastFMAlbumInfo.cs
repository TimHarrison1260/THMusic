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
    }


    public class LastFMTrack
    {
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
        public string name { get; set; }
        public string url { get; set; }
        public string mbid { get; set; }
    }

    public class LastFMArtwork
    {
        public string size { get; set; }
        public string imageUrl { get; set; }
    }

    public class LastFMTag
    {
        public string name { get; set; }
        public string url { get; set; }
    }


    public class LastFMWiki
    {
        public string published { get; set; }
        public string summary {get; set;}
        public string content { get; set; }
    }



    public class LastFMError
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
