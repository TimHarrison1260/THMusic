//***************************************************************************************************
//Name of File:     ArtistRepository.cs
//Description:      The implementation of the interface describing the PlaylistRepository.
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
    /// This <c>PlaylistRepository</c> class is responsible for managing access to the
    /// the In-Memory context for the Music collection, specifially for the Playlists
    /// contained within the collection.
    /// </summary>
    /// <remarks>
    /// This class inherits from the <see cref="Infrastructure.Repositories.BaseRepository"/>
    /// which is generic.  It passes the Artist class in as the type.
    /// <para>
    /// It also implements the IPlaylsitRepository interface which separates the concerns from
    /// the rest of the application, from this implementation.  The IPLaylistRepository interface,
    /// in turn inherits from the IGroupRepository.  This base interface defines the methods 
    /// required for this interface and allows the instance of this repository to be passed 
    /// into generic routines that expect an implementation of the IGroupRepository interface.
    /// There are 3 such repositories, namely Artist, Genre and Playlist.  These are all 
    /// classes and associated repositories involved in grouping albums together.
    /// </para>
    /// </remarks>
    public class PlaylistRepository: BaseRepository<PlayList>, IPlaylistRepository
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
        public PlaylistRepository(IUnitOfWork UnitOfWork)
            : base()
        {
            if (UnitOfWork == null)
                throw new ArgumentNullException("UnitOrWork", "No valid UnitOfWork defined for ArtistRepository");
            _unitOrWork = UnitOfWork;
        }

        /// <summary>
        /// Gets all artists defined in the Context
        /// </summary>
        /// <returns>An IEnumerable collection of Artist classes</returns>
        public async Task<IEnumerable<PlayList>> GetAll()
        {
            var playlists = _unitOrWork.PlayLists;
            return playlists;
        }

        /// <summary>
        /// Gets the Image to use for the Artist Grouping.
        /// It returns the first image representing an
        /// Album cover, from the albums belonging to 
        /// this Artist.
        /// </summary>
        /// <param name="Id">The Id of the Artist</param>
        /// <returns>A string containing the path to the relevant image.</returns>
        public async Task<string> GetFirstAlbumImage(int Id)
        {
            var image = _unitOrWork.PlayLists
                .Where(p => p.Id == Id)
                .SelectMany(p => p.Tracks)
                .SelectMany(t => t.Album.Images)
                .Where(i => i.Size == ImageSizeEnum.large)
                .FirstOrDefault();
            return image.Url;
        }

        /// <summary>
        /// Gets the total Duration of all albums and tracks
        /// belonging to the artist, for the Artist Group.
        /// </summary>
        /// <param name="Id">The Id of the Artist</param>
        /// <returns>The total duration of the albums and tracks</returns>
        public async Task<TimeSpan> GetDuration(int Id)
        {
            var duration = _unitOrWork.PlayLists
                .Where(p => p.Id == Id)
                .SelectMany(p => p.Tracks)
                .Sum(t => t.Duration.Ticks);

            return TimeSpan.FromTicks(duration);
        }

        /// <summary>
        /// Gets the number of albums belonging to the Artist
        /// </summary>
        /// <param name="Id">The Id of the Artist</param>
        /// <returns>the count of albums belonging to the Artist</returns>
        public async Task<int> GetAlbums(int Id)
        {
            var number = _unitOrWork.PlayLists
                .Where(p => p.Id == Id)
                .SelectMany(p => p.Tracks)
                .Select(t => t.Album)
                .Count();
                
            return number;
        }

        /// <summary>
        /// Gets the number of tracks belonging to the Artist
        /// </summary>
        /// <param name="Id">The Id of the Artist.</param>
        /// <returns>The count of tracks belonging to the Artist</returns>
        public async Task<int> GetTracks(int Id)
        {
            //  This query will not support parallelism, as it makes use of
            //  the SelectMany statement which is not supported by PLinq yet.
            var number = _unitOrWork.PlayLists
                .Where(p => p.Id == Id)
                .SelectMany(p => p.Tracks)
                .Count();
            return number;
        }

        /// <summary>
        /// Gets an Artist class for the specified Artist Id
        /// </summary>
        /// <param name="Id">The id of the Artist</param>
        /// <returns>The Artist object</returns>
        public async Task<PlayList> GetById(int Id)
        {
            //  Difficult to see how parallelism would benefit here,
            //  only a single artist is to be returned
            var playlist = _unitOrWork.PlayLists
                .FirstOrDefault(p => p.Id == Id);
            return playlist;
        }

        /// <summary>
        /// Gets an Artist class for the specified Artist Name
        /// </summary>
        /// <param name="Name">the Name of the Artist</param>
        /// <returns>The Artist object</returns>
        public async Task<PlayList> GetByName(string Name)
        {
            throw new NotImplementedException();
        }


    }
}
