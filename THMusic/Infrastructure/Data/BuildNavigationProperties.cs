//***************************************************************************************************
//Name of File:     BuildNavigationProperties.cs
//Description:      It builds the navigation properties within the Album and Tracks for the model 
//                  classes once the MusicCollection context has been loaded at the startup of the 
//                  application.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core.Interfaces;

namespace Infrastructure.Data
{
    /// <summary>
    /// This <c>BuildNavigationProperties</c> class is responsible for 
    /// building the navigation properties in the various classes of
    /// the <see cref="Infrastructure.Data.MusicCollection"/> once the 
    /// data has been loaded from the xml file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It uses heavily the routines provided by the 
    /// <see cref="Infrastructure.Data.DataHelper"/> class.  There are
    /// many supporting methods in this class and the logic is quite complex.
    /// </para>
    /// </remarks>
    public class BuildNavigationProperties
    {
        //  Hold the reference to the helper class that does all the work
        //  of actually updating the rest of the collections in the Domain
        //  Model and applying the corred update logic.
        private DataHelper _helper = new DataHelper();

        /// <summary>
        /// Builds the navigation properties of the the Album and Associated Tracks
        /// </summary>
        /// <param name="musicCollection">The In-Memory Data Context</param>
        /// <remarks>
        /// This is method is marked async, but there are no async calls at the
        /// moment.  Investigate the use of plinq, if it supports all the linq
        /// extensions used.  If it does then use them.  UNLIKELY, because the
        /// stuff is only single calls and the data calls cannot be separated.
        /// </remarks>
        public async Task Build(IUnitOfWork musicCollection)
        {
            //  Process each album class looking for the related classes
            //  and navigation properties.
            foreach (var inAlbum in musicCollection.Albums)
            {
                //  Load the Tracks, with a reference to the album and the Playlists.
                var updatedTracks = _helper.AddTracksToContext(musicCollection, inAlbum.Tracks, inAlbum);
                inAlbum.Tracks = updatedTracks;

                //  Replace the Genres with those from the Genres collection as the ones in the 
                //  album have the references to the albums missing.
                var updatedGenres = _helper.AddGenresToContext(musicCollection, inAlbum.Genres, inAlbum);
                inAlbum.Genres = updatedGenres;

                //  Replace the artist with the one from the artists collection, as the album one 
                //  has the album information missing.
                var updatedArtist = _helper.AddArtistToContext(musicCollection, inAlbum.Artist, inAlbum);
                inAlbum.Artist = updatedArtist;
            }
        }
    }
}
