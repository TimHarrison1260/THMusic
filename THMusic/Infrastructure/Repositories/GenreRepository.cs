//***************************************************************************************************
//Name of File:     GenreRepository.cs
//Description:      The implementation of the interface describing the GenreRepository.
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
    /// This <c>GenreRepository</c> class is responsible for managing access to the
    /// the In-Memory context for the Music collection, specifially for the Genres
    /// contained within the collection.
    /// </summary>
    /// <remarks>
    /// This class inherits from the <see cref="Infrastructure.Repositories.BaseRepository"/>
    /// which is generic.  It passes the Artist class in as the type.
    /// <para>
    /// It also implements the IGenreRepository interface which separates the concerns from
    /// the rest of the application, from this implementation.  The IGenreRepository interface,
    /// in turn inherits from the IGroupRepository.  This base interface defines the methods 
    /// required for this interface and allows the instance of this repository to be passed 
    /// into generic routines that expect an implementation of the IGroupRepository interface.
    /// There are 3 such repositories, namely Artist, Genre and Playlist.  These are all 
    /// classes and associated repositories involved in grouping albums together.
    /// </para>
    /// </remarks>
    public class GenreRepository: BaseRepository<Genre>, IGenreRepository
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
        public GenreRepository(IUnitOfWork UnitOfWork)
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
        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = _unitOrWork.Genres;
            return genres;
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
            var image = _unitOrWork.Genres
                .Where(g => g.Id == Id)
                .SelectMany(a => a.Albums)
                .Where(a => a.Images.Count() > 0)
                .SelectMany(a => a.Images)
                .Where(i => i.Size == ImageSizeEnum.large)
                .First();
            return image.Url;
        }

        /// <summary>
        /// Gets the total Duration of all albums and tracks
        /// belonging to the artist, for the Artist Group.a => a
        /// </summary>
        /// <param name="Id">The Id of the Artist</param>
        /// <returns>The total duration of the albums and tracks</returns>
        public async Task<TimeSpan> GetDuration(int Id)
        {
            //  This query will not support parallelism, as it makes use of
            //  the SelectMany statement which is not supported by PLinq yet.

            //var duration = _unitOrWork.Albums
            //    .Where(a => a.Artist.Id == Id)
            //    .SelectMany(a => a.Tracks)
            //    .Sum(t => t.Duration.Ticks);

            var duration = _unitOrWork.Genres
                .Where(g => g.Id == Id)
                .SelectMany(a => a.Albums)
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
            //var number = _unitOrWork.Albums.AsParallel()
            //    .Where(a => a.Artist.Id == Id)
            //    .Count();

            var number = _unitOrWork.Genres
                .Where(g => g.Id == Id)
                .SelectMany(a => a.Albums)
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
            //var number = _unitOrWork.Albums
            //    .Where(a => a.Artist.Id == Id)
            //    .SelectMany(a => a.Tracks)
            //    .Count();

            var number = _unitOrWork.Genres
                .Where(g => g.Id == Id)
                .SelectMany(g => g.Albums)
                .SelectMany(a => a.Tracks)
                .Count();
            return number;
        }

        /// <summary>
        /// Gets an Artist class for the specified Artist Id
        /// </summary>
        /// <param name="Id">The id of the Artist</param>
        /// <returns>The Artist object</returns>
        public async Task<Genre> GetById(int Id)
        {
            //  Difficult to see how parallelism would benefit here,
            //  only a single artist is to be returned
            var genre = _unitOrWork.Genres
                .FirstOrDefault(g => g.Id == Id);
            return genre;
        }

        /// <summary>
        /// Gets an Artist class for the specified Artist Name
        /// </summary>
        /// <param name="Name">the Name of the Artist</param>
        /// <returns>The Artist object</returns>
        public async Task<Genre> GetByName(string Name)
        {
            throw new NotImplementedException();
        }


    }
}
