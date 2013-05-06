//***************************************************************************************************
//Name of File:     IArtistRepository.cs
//Description:      An interface describing the ArtistRepository.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Interfaces
{
    public interface IArtistRepository
    {
        /// <summary>
        /// Get all Artists
        /// </summary>
        /// <returns>A collection of Artists</returns>
        Task<IEnumerable<Artist>> GetAllArtists();
        /// <summary>
        /// Gets the first Album cover image belonging to the artist
        /// </summary>
        /// <param name="Id">The Id of the Artist</param>
        /// <returns>Path to the album cover image</returns>
        Task<string> GetArtistAlbumImage(int Id);
        /// <summary>
        /// Gets a count of albums beloning to the Artist
        /// </summary>
        /// <param name="Id">The Id of the Artist</param>
        /// <returns>the count of albums</returns>
        Task<int> GetArtistAlbums(int Id);
        /// <summary>
        /// Gets a count of the tracks belonging to the Artist
        /// </summary>
        /// <param name="Id">The Id of the Artist</param>
        /// <returns>the count of Tracks</returns>
        Task<int> GetArtistTracks(int Id);
        /// <summary>
        /// Gets the total duration of all tracks belonging to the Artist.
        /// </summary>
        /// <param name="Id">the Id of the Artist.</param>
        /// <returns>The total Duration</returns>
        Task<TimeSpan> GetArtistDuration(int Id);
        /// <summary>
        /// Gets an Artist by Id
        /// </summary>
        /// <param name="Id">the Id of the Artist</param>
        /// <returns>The Artist class</returns>
        Task<Artist> GetArtistById(int Id);
        /// <summary>
        /// Gets an Artist by Artist name
        /// </summary>
        /// <param name="Name">The Name of the Artist</param>
        /// <returns>The Artist class.</returns>
        Task<Artist> GetArtistByName(string Name);
    }
}
