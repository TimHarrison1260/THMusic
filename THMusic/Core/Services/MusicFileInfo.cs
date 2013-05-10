//***************************************************************************************************
//Name of File:     MusicFileInfo.cs
//Description:      The MusicFileInfo provides the tag information from a file. (Taglig# output)
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
namespace Core.Services
{
    /// <summary>
    /// This <c>MusicFileInfo</c> class describes the tagged inforamtion
    /// extracted from a music file.
    /// </summary>
    /// <remarks>
    /// Use of this <c>MusicFileInfo</c> class is preferred to simply passing
    /// the Tagtlib.file type up the calling chain, because it allows for a 
    /// different tagging library to be used without changing the rest of the
    /// application.
    /// </remarks>
    public class MusicFileInfo 
    {
        /// <summary>
        /// Ctor: MusicFileInfo class.
        /// </summary>
        public MusicFileInfo()
        {
            this.TrackName = string.Empty;
            this.TrackNumber = 0;
            this.TrackDuration = new TimeSpan();
            this.TrackMbid = string.Empty;

            this.ArtistName = string.Empty;
            this.ArtistMbid = string.Empty;

            this.Genres = new List<string>();

            this.AlbumTitle = string.Empty;
            this.AlbumReleased = new DateTime();
            this.AlbumMbid = string.Empty;
        }

        //  Track information
        public string TrackName { get; set; }           //  tag.Title
        public int TrackNumber { get; set; }            //  tag.Track
        public TimeSpan TrackDuration { get; set; }     //  duration => timespan
        public string TrackMbid { get; set; }           //  tag.MusicBrainzTrackid

        //  Related Artist Information
        public string ArtistName { get; set; }          //  Artists[0], or FirstAlbumArtist
        public string ArtistMbid { get; set; }          //  tag.MusicBrainzArtistId
        
        //  Related Genres
        public IList<string> Genres { get; set; }       //  tag.Genres, or tag.FirstGenre

        //  Related Album information
        public string AlbumTitle { get; set; }          //  tag.Album
        public DateTime AlbumReleased { get; set; }     //  tag.Released => Year only
        public string AlbumMbid { get; set; }           //  tag.MusicBrainzDiscId

    }
}
