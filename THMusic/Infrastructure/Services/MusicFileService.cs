//***************************************************************************************************
//Name of File:     MusicFileService.cs
//Description:      The MusicFileService provides the taglib functionality, abstracting it from the 
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

using Windows.Storage;          //  Windows store app file api
using TagLib;                   //  Media file tag extractor
using System.IO;                //  File IO stuff
using Windows.Storage.Streams;  //  File stream stuff

using Core.Services;
using Infrastructure.Helpers;

namespace Infrastructure.Services
{
    /// <summary>
    /// This <c>MusicFileService</c> class is responsible for extracting the tagged
    /// information about a supplied music file. 
    /// </summary>
    /// <remarks>
    /// It makes use of the Taglib-Sharp tagging framework to access the tagged informtion
    /// on the file.  
    /// </remarks>
    public class MusicFileService : IMusicFileService
    {
        /// <summary>
        /// This <c>GetMusicFileInfoAsync</c>  is responsible for extracting the 
        /// tagged information from the supplied file, then mapping the results
        /// to an instance of the <see cref="Core.Services.MusicFileInfo"/> class.
        /// </summary>
        /// <param name="file">The file to be tagged.  This is an instance of 
        /// a <see cref="Windows.Storage.StorageFile"/> .</param>
        /// <returns>The extracted information as an instance of the MusicFileInfo class.</returns>
        public async Task<MusicFileInfo> GetMusicFileInfoAsync(StorageFile file)
        {
            // call the helper to retrieve the taglib information
            TagLib.File taggedInfo = await MusicFileHelper.GetMusicFileInfoAsync(file);

            //  construct the MusicFileInfo from the output, adding in the necessary information
            //  from the actual file object.
            var musicFileInfo = MapTaglibFileToMusicFileInfo(taggedInfo);

            //  Return the populated object.
            return musicFileInfo;
        }

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
        private MusicFileInfo MapTaglibFileToMusicFileInfo(TagLib.File taggedFile)
        {
            var musicFile = new MusicFileInfo();
            
            //  Track
            if (taggedFile.Tag.Title != null)
                musicFile.TrackName = taggedFile.Tag.Title;
            if (taggedFile.Tag.Track != null)
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
            if (taggedFile.Tag.Year != null)
                musicFile.AlbumReleased = new DateTime(Convert.ToInt32(taggedFile.Tag.Year), 1, 1);
            if (taggedFile.Tag.MusicBrainzDiscId != null)
                musicFile.AlbumMbid = taggedFile.Tag.MusicBrainzDiscId;

            return musicFile;
        }


    }
}
