//***************************************************************************************************
//Name of File:     MusicFileMapper.cs
//Description:      The MusicFileMapper maps the taglib file contents to the MusicFileInfo class
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Linq;

using Core.Services;

namespace Infrastructure.Services
{
    /// <summary>
    /// this <c>MusicFileMapper</c> class is responsible for mapping the 
    /// Taglib# file format to the one used within this application
    /// A custom class is returned instead of the taglib.file class so 
    /// that a different tagging library could easily be substituted.
    /// </summary>
    public static class MusicFileMapper
    {
        /// <summary>
        /// Maps the Taglib file to the required MusicFileInfo class.
        /// </summary>
        /// <param name="taggedFile">Teh tagged taglib file</param>
        /// <returns>A MusicFileInfo class</returns>
        /// <remarks>
        /// This copy kept as a private routing within this class as
        /// it is not used anywhere else within the application and is
        /// specific to this MusicFileService.
        /// </remarks>
        public static MusicFileInfo MapTaglibFileToMusicFileInfo(TagLib.File taggedFile)
        {
            var musicFile = new MusicFileInfo();

            //  Track
            if (taggedFile.Tag.Title != null)
                musicFile.TrackName = taggedFile.Tag.Title;
            //if (taggedFile.Tag.Track != null)
                musicFile.TrackNumber = Convert.ToInt32(taggedFile.Tag.Track);
            if (taggedFile.Properties.Duration != null)
                musicFile.TrackDuration = taggedFile.Properties.Duration;
            if (taggedFile.Tag.MusicBrainzTrackId != null)
                musicFile.TrackMbid = taggedFile.Tag.MusicBrainzTrackId;

            //  Artist
            if (taggedFile.Tag.AlbumArtists.Count() > 0)
                musicFile.ArtistName = taggedFile.Tag.AlbumArtists[0];
            else
            {
                if (taggedFile.Tag.Performers.Count() > 0)
                    musicFile.ArtistName = taggedFile.Tag.Performers[0];
                else
                {
                    //  The Artists is marked as obsolete, use it in the
                    //  event an old file is being processed and the 
                    //  up to date information is not available.
                    //  Shouldn't get here really.
                    if (taggedFile.Tag.Artists.Count() > 0)
                        musicFile.ArtistName = taggedFile.Tag.Artists[0];
                    else
                        musicFile.ArtistName = "No Artist Found for Media file";
                }

            }
            if (taggedFile.Tag.MusicBrainzArtistId != null)
                musicFile.ArtistMbid = taggedFile.Tag.MusicBrainzArtistId;

            //  Genres
            if (taggedFile.Tag.Genres.Count() > 0)
            {
                foreach (var tag in taggedFile.Tag.Genres)
                {
                    musicFile.Genres.Add(tag);
                }
            }

            //  Album information
            if (taggedFile.Tag.Album != null)
                musicFile.AlbumTitle = taggedFile.Tag.Album;
            //if (taggedFile.Tag.Year != null)
                musicFile.AlbumReleased = new DateTime(Convert.ToInt32(taggedFile.Tag.Year), 1, 1);
            if (taggedFile.Tag.MusicBrainzDiscId != null)
                musicFile.AlbumMbid = taggedFile.Tag.MusicBrainzDiscId;

            return musicFile;
        }
    }
}
