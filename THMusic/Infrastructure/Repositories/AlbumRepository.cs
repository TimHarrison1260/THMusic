//***************************************************************************************************
//Name of File:     AlbumRepository.cs
//Description:      The implementation of the interface describing the AlbumRepository.
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

using Core.Interfaces;
using Core.Model;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// This <c>AlbumRepository</c> class is responsible for managing access to the
    /// the In-Memory context for the Music collection, specifially for the Albums
    /// contained within the collection.
    /// </summary>
    /// <remarks>
    /// This class inherits from the <see cref="Infrastructure.Repositories.BaseRepository"/>
    /// which is generic.  It passes the Artist class in as the type.
    /// It also implements the IAlbumRepository interface which separates the concerns from
    /// the rest of the application, from this implementation.
    /// </remarks>
    public class AlbumRepository: BaseRepository<Album>, IAlbumRepository
    {
        /// <summary>
        /// Contains the context for the datastore (loaded from the XML file)
        /// </summary>
        private readonly IUnitOfWork _unitOrWork;

        /// <summary>
        /// ctor: Accepts the instance of the UnitOfWork the In-Memory context, injected by
        /// the MVVMLight SimpleIoC container.
        /// </summary>
        /// <param name="UnitOfWork">Injected instance of the Context</param>
        public AlbumRepository(IUnitOfWork UnitOfWork):base()
        {
            if (UnitOfWork == null)
                throw new ArgumentNullException("UnitOrWork", "No valid UnitOfWork defined for AlbumRepository");
            _unitOrWork = UnitOfWork;
        }

        /// <summary>
        /// Gets all Albums defined in the Music Collection context.
        /// </summary>
        /// <returns>An IEnumerable collection of Album classes</returns>
        public async Task<IEnumerable<Album>> GetAllAlbums()
        {
            var albums = _unitOrWork.Albums;
            return albums;
        }

        /// <summary>
        /// Gets all Albums belonging to the specified Artist.
        /// </summary>
        /// <param name="ArtistId">The Id of the Artist</param>
        /// <returns>A collection of Albums</returns>
        public async Task<IEnumerable<Album>> GetAlbumsForArtist(int ArtistId)
        {
            //  Execute the linq query as a parallel query, to improve async behaviour
            //  NB! This does NOT remove the warning about the method 'lacking await'
            //      and therefore will run synchronously.
            var albums = _unitOrWork.Albums.AsParallel()
                .Where(a => a.Artist.Id == ArtistId);
            return albums;
        }

        /// <summary>
        /// Creates a New Album in the Domain model
        /// </summary>
        /// <param name="entity">The album to be created</param>
        /// <returns>The new instance of the Album</returns>
        public override async Task<Album> CreateAsync(Album entity)
        {
            var newalbum = await _unitOrWork.CreateAlbum(entity);
            return newalbum;  
        }

        /// <summary>
        /// Gets all Albums belonging to the specified Genre.
        /// </summary>
        /// <param name="GenreId">The Id of the Genre.</param>
        /// <returns>a collection of Album classes.</returns>
        public async Task<IEnumerable<Album>> GetAlbumsForGenre(int GenreId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all Albums belonging to the specified Playlist
        /// </summary>
        /// <param name="PlaylistId">The Id of the Playlist.</param>
        /// <returns>A collection of Albums</returns>
        public async Task<IEnumerable<Album>> GetAlbumsForPlaylist(int PlaylistId)
        {
            throw new NotImplementedException();
        }
    }
}
