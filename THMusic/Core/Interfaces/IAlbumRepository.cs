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
    /// <summary>
    /// This <c>IAlbumRepository</c> interface describes the contract
    /// for the AlbumRepository
    /// </summary>
    public interface IAlbumRepository
    {
        /// <summary>
        /// Get all albums in the collection
        /// </summary>
        /// <returns>Returns the complete colledtion of Albums</returns>
        Task<IEnumerable<Album>> GetAllAlbums();
        /// <summary>
        /// Gets the albums for an Artist
        /// </summary>
        /// <param name="ArtistId">The Id of the Artist</param>
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
        /// Creates a new album in the domain.
        /// </summary>
        /// <param name="album">The album to be added to the domain</param>
        /// <returns>The instance of the newly created album</returns>
        Task<Album> CreateAsync(Album album);
        /// <summary>
        /// Check if a mediafile, specified by its absolute path, has
        /// already been imported into the collection.
        /// </summary>
        /// <param name="mediaFilePath">The path of the mediafile</param>
        /// <returns>Returns <c>true</c> if it has, otherwise <c>false</c></returns>
        Task<bool> IsMediaFileImported(string mediaFilePath);
        /// <summary>
        /// Check if an album has already been imported into the Music
        /// Collection.  It checks the Album name and the Artist name.
        /// </summary>
        /// <param name="ArtistName">Artists Name</param>
        /// <param name="AlbumName">album Name</param>
        /// <returns>Returns the Album if it exists, otherwise it returns NULL</returns>
        Task<Album> IsAlbumAlreadyImported(string ArtistName, string AlbumName);

        /// <summary>
        /// Add a new track to an existing album.
        /// </summary>
        /// <param name="Entity">The new track, encapsulated within an album</param>
        /// <returns>An instance of Task</returns>
        Task AddTrackToAlbum(Album Entity);
    }
}
