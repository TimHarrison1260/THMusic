//***************************************************************************************************
//Name of File:     ArtistRepository.cs
//Description:      The implementation of the interface describing the ArtistRepository.
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
    /// This <c>ArtistRepository</c> class is responsible for managing access to the
    /// the In-Memory context for the Music collection, specifially for the Artists
    /// contained within the collection.
    /// </summary>
    /// <remarks>
    /// This class inherits from the <see cref="Infrastructure.Repositories.BaseRepository"/>
    /// which is generic.  It passes the Artist class in as the type.
    /// <para>
    /// It also implements the IArtistRepository interface which separates the concerns from
    /// the rest of the application, from this implementation.  The IArtistRepository interface,
    /// in turn inherits from the IGroupRepository.  This base interface defines the methods 
    /// required for this interface and allows the instance of this repository to be passed 
    /// into generic routines that expect an implementation of the IGroupRepository interface.
    /// There are 3 such repositories, namely Artist, Genre and Playlist.  These are all 
    /// classes and associated repositories involved in grouping albums together.
    /// </para>
    /// </remarks>
    public class ArtistRepository: BaseRepository<Artist>, IArtistRepository
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
        public ArtistRepository(IUnitOfWork UnitOfWork)
            :base()
        {
            if (UnitOfWork == null)
                throw new ArgumentNullException("UnitOrWork", "No valid UnitOfWork defined for ArtistRepository");
            _unitOrWork = UnitOfWork;
        }

        /// <summary>
        /// Gets all artists defined in the Context
        /// </summary>
        /// <returns>An IEnumerable collection of Artist classes</returns>
        public async Task<IEnumerable<Artist>> GetAll()
        {
            var artists = _unitOrWork.Artists;
            return artists;
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
            //  This query will not support parallelism, as it makes use of
            //  the SelectMany statement which is not supported by PLinq yet.
            var image = _unitOrWork.Albums
                .Where(a => a.Artist.Id == Id)
                .SelectMany(a => a.Images)
                .Where(i => i.Size == ImageSizeEnum.large)
                .First();
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
            //  This query will not support parallelism, as it makes use of
            //  the SelectMany statement which is not supported by PLinq yet.
            var duration = _unitOrWork.Albums
                .Where(a => a.Artist.Id == Id)
                .SelectMany(a => a.Tracks)
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
            var number = _unitOrWork.Albums.AsParallel()
                .Where(a => a.Artist.Id == Id)
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
            var number = _unitOrWork.Albums
                .Where(a => a.Artist.Id == Id)
                .SelectMany(a => a.Tracks)
                .Count();
            return number;
        }

        /// <summary>
        /// Gets an Artist class for the specified Artist Id
        /// </summary>
        /// <param name="Id">The id of the Artist</param>
        /// <returns>The Artist object</returns>
        public async Task<Artist> GetById(int Id)
        {
            //  Difficult to see how parallelism would benefit here,
            //  only a single artist is to be returned
            var artist = _unitOrWork.Artists
                .FirstOrDefault(a => a.Id == Id);
            return artist;
        }

        /// <summary>
        /// Gets an Artist class for the specified Artist Name
        /// </summary>
        /// <param name="Name">the Name of the Artist</param>
        /// <returns>The Artist object</returns>
        public async Task<Artist> GetByName(string Name)
        {
            throw new NotImplementedException();
        }


    }
}
