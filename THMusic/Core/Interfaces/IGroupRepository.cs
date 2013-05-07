//***************************************************************************************************
//Name of File:     IGroupRepository.cs
//Description:      An interface describing the base interface for IArtist, IGenre and IPlaylist repositories.
//                  It serves as the base interface IGroupRepository.  This gives all classes
//                  that participate in the "Groupin" of album, the same functionality so that
//                  the repositories can be cast and interchanged, due to inheritance
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

namespace Core.Interfaces
{
    /// <summary>
    /// This <c>IGroupRepository</c> interface describes the common interface 
    /// for all interfaces that derive from it.  It is intended as the base
    /// interface for all repositories that provide grouping of albums for
    /// the UI.
    /// </summary>
    /// <typeparam name="T">The type of class the interface uses.</typeparam>
    public interface IGroupRepository<T> where T: class
    {
        /// <summary>
        /// Get all Artists
        /// </summary>
        /// <returns>A collection of Artists</returns>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Gets the first Album cover image belonging to the artist
        /// </summary>
        /// <param name="Id">The Id of the Artist</param>
        /// <returns>Path to the album cover image</returns>
        Task<string> GetFirstAlbumImage(int Id);
        /// <summary>
        /// Gets a count of albums beloning to the Artist
        /// </summary>
        /// <param name="Id">The Id of the Artist</param>
        /// <returns>the count of albums</returns>
        Task<int> GetAlbums(int Id);
        /// <summary>
        /// Gets a count of the tracks belonging to the Artist
        /// </summary>
        /// <param name="Id">The Id of the Artist</param>
        /// <returns>the count of Tracks</returns>
        Task<int> GetTracks(int Id);
        /// <summary>
        /// Gets the total duration of all tracks belonging to the Artist.
        /// </summary>
        /// <param name="Id">the Id of the Artist.</param>
        /// <returns>The total Duration</returns>
        Task<TimeSpan> GetDuration(int Id);
        /// <summary>
        /// Gets a Group by Id
        /// </summary>
        /// <param name="Id">the Id of the Artist</param>
        /// <returns>The Artist class</returns>
        Task<T> GetById(int Id);
        /// <summary>
        /// Gets a Group by name
        /// </summary>
        /// <param name="Name">The Name of the Artist</param>
        /// <returns>The Artist class.</returns>
        Task<T> GetByName(string Name);
    }
}
