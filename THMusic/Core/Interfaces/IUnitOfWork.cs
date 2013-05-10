//***************************************************************************************************
//Name of File:     IUnitOfWork.cs
//Description:      An interface describing the UnitOfWork pattern, to inject the instance of the
//                  data context and control it's lifetime.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

using Core.Model;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// The collection of Albums making up the Collection
        /// </summary>
        List<Album> Albums { get; set; }

        /// <summary>
        /// The Tracks belonging to the Aobums in the Collection
        /// </summary>
        List<Track> Tracks { get; set; }

        /// <summary>
        /// The Artists that have albums in the collection
        /// </summary>
        List<Artist> Artists { get; set; }

        /// <summary>
        /// The various Genres associated with album in the collection
        /// </summary>
        List<Genre> Genres { get; set; }

        /// <summary>
        /// The various playlist containing albums and tracks from the collection
        /// </summary>
        List<PlayList> PlayLists { get; set; }

        /// <summary>
        /// Adds the new album to the In-Memory context, updating the related collections.
        /// </summary>
        /// <param name="newAlbum">The New album to be created</param>
        /// <returns>The instance of the newly created album.</returns>
        Task<Album> CreateAlbum(Album newAlbum);

        /// <summary>
        /// Add a new track to an existing album in the In-Memory context, updating the related 
        /// entities and ensuring the navigation properties are correctly updated.
        /// </summary>
        /// <param name="UpdatedAlbum">The new track, encapsulated within an album</param>
        Task AddTrackToAlbum(Album UpdatedAlbum);

        /// <summary>
        /// Gets or sets the reference to the storage file while the context is alive.
        /// </summary>
        StorageFile PersistenceFile { get; set; }
    }
}
