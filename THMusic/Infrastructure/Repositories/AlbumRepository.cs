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

using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;



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
            var albums = _unitOrWork.Albums
                .Where(a => a.Artist.Id == ArtistId);
            return albums;
        }

        /// <summary>
        /// Gets all Albums belonging to the specified Genre.
        /// </summary>
        /// <param name="GenreId">The Id of the Genre.</param>
        /// <returns>a collection of Album classes.</returns>
        public async Task<IEnumerable<Album>> GetAlbumsForGenre(int GenreId)
        {
            var albums = _unitOrWork.Genres
                .Where(g => g.Id == GenreId)
                .SelectMany(g => g.Albums);
            return albums;
        }

        /// <summary>
        /// Get all Albums belonging to the specified Playlist
        /// </summary>
        /// <param name="PlaylistId">The Id of the Playlist.</param>
        /// <returns>A collection of Albums</returns>
        public async Task<IEnumerable<Album>> GetAlbumsForPlaylist(int PlaylistId)
        {
            //  Horrible bit of linq and process here, to supply for each album
            //  belonging to a track in the playlist, contining only those 
            //  tracks in the playlist.

            //  Get tracks.
            var tracks = _unitOrWork.PlayLists
                .Where(p => p.Id == PlaylistId)
                .SelectMany(p => p.Tracks);
            //  Get a distinct list of albums

            //  NB:  THIS WILL ACTUALLY UPDATE THE CONTEXT, NOT WHAT'S NEEDED
            //      THEREFORE MAKE A COMPLETE COPY OF THE SELECTED ALBUMS
            //      AND FORCE THE LINQ QUERY TO ENUMERATE THE COLLECTION
            //      TO ENSURE IT RUNS NOW AND WE GET A COMPLETELY NEW INSTANCE.
            //      A shallow copy is enough, here, but without the tracks, these
            //      must be a completely new instance that can have only the 
            //      selected tracks added.  This completely avoids unintended
            //      updating of the DomainModel.  the album is a complely new
            //      instance, but the referenced objects are not new, except f
            //      for the tracks.
            var albums = tracks
                .Select(a => a.Album)
                .Distinct();

            //  Create a list of cloned albums, for the playlists.
            IList<Album> playlistAlbums = new List<Album>();
            foreach (var a in albums)
            {
                var clonedAlbum = a.Clone();
                playlistAlbums.Add(clonedAlbum);
            }

            //  Now set the album.Tracks property to only contain the above tracks
            foreach (var playlistAlbum in playlistAlbums)
            {
                var playlistTracks = tracks.Where(t => t.Album.Id == playlistAlbum.Id);
                playlistAlbum.Tracks = playlistTracks.ToList<Track>();
            }

            //return albums;

            return playlistAlbums;
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
        /// Check if a mediafile, specified by its absolute path, has
        /// already been imported into the collection.
        /// </summary>
        /// <param name="mediaFilePath">The path of the mediafile</param>
        /// <returns>Returns <c>true</c> if it has, otherwise <c>false</c></returns>
        public async Task<bool> IsMediaFileImported(string mediaFilePath)
        {
            var result = _unitOrWork.Tracks
                .FirstOrDefault(t => t.mediaFilePath == mediaFilePath);
            return (result != null) ? true : false;
        }

        /// <summary>
        /// Check if an album has already been imported into the Music
        /// Collection.  It checks the Album name and the Artist name.
        /// </summary>
        /// <param name="ArtistName">Artists Name</param>
        /// <param name="AlbumName">album Name</param>
        /// <returns>Returns the Album if it exists, otherwise it returns NULL</returns>
        public async Task<Album> IsAlbumAlreadyImported(string ArtistName, string AlbumName)
        {
            var album = _unitOrWork.Albums
                .FirstOrDefault(a => a.Title == AlbumName && a.Artist.Name == ArtistName);
            return album;
        }

        /// <summary>
        /// Add a new track to an existing album.
        /// </summary>
        /// <param name="Entity">The new track, encapsulated within an album</param>
        /// <returns>A Task so that the method is awaitable</returns>
        public async Task AddTrackToAlbum(Album Entity)
        {
            await _unitOrWork.AddTrackToAlbum(Entity);
        }

    }
}
