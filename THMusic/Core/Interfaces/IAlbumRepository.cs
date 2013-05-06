//***************************************************************************************************
//Name of File:     IAlbumRepository.cs
//Description:      An interface describing the AlbumRepository.
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
    public interface IAlbumRepository
    {
        /// <summary>
        /// Get all albums in the collection
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Album>> GetAllAlbums();
        /// <summary>
        /// Gets the albums for an Artist
        /// </summary>
        /// <param name="ArtistId">The Idof the Artist</param>
        /// <returns>A collection of Albums</returns>
        Task<IEnumerable<Album>> GetAlbumsForArtist(int ArtistId);
        /// <summary>
        /// Get the albums for a Genre
        /// </summary>
        /// <param name="GenreId">the Id of the Genre</param>
        /// <returns>A collection of Albums</returns>
        Task<IEnumerable<Album>> GetAlbumsForGenre(int GenreId);
        /// <summary>
        /// Get the albums for a Playlist
        /// </summary>
        /// <param name="PlaylistId">The id of the Playlist</param>
        /// <returns>A collection of Albums</returns>
        Task<IEnumerable<Album>> GetAlbumsForPlaylist(int PlaylistId);
        /// <summary>
        /// Creates a new album in the donam
        /// </summary>
        /// <param name="album">The album to be added to the domain</param>
        /// <returns>The instance of the newly created album</returns>
        Task<Album> CreateAsync(Album album);
    }
}
