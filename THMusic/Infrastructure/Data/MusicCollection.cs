//***************************************************************************************************
//Name of File:     MusicCollection.cs
//Description:      The in-memory data context.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.IO;
using Windows.Storage;
using System.Xml.Serialization;

using Core.Interfaces;
using Core.Model;
using Core.Factories;

using GalaSoft.MvvmLight.Ioc;


namespace Infrastructure.Data
{
    /// <summary>
    /// This <c>MusicCollection</c> class contains the In-Memory instances of the Domain model
    /// classes and collections.
    /// It implements the IUnitOfWork, by which the SimpleIoC mechanism of MVVMLight
    /// framework injects the instance of this class.  It is able then, to control the
    /// lifetime of this class ensuring that the same instance is always used.
    /// this is the Unit Of Work pattern.
    /// </summary>
    public class MusicCollection : IUnitOfWork
    {
        /// <summary>
        /// Holds the instance of the file handle to the underlying datafile.
        /// </summary>
        private StorageFile _dataFile = null;

        //  Instances of the factories required to create individual objects.
        private readonly AbstractFactory<Artist> _artistFactory;
        private readonly AbstractFactory<Album> _albumFactory;
        private readonly AbstractFactory<Track> _trackFactory;
        private readonly AbstractFactory<Image> _imageFactory;
        private readonly AbstractFactory<Genre> _genreFactory;
        private readonly AbstractFactory<PlayList> _playlistFactory;
        private readonly AbstractFactory<Wiki> _wikiFactory;

        private DataHelper _helper = new DataHelper();

        /// <summary>
        /// ctor: parameterless constructor required by XMLSerialisation
        /// </summary>
        public MusicCollection()
        {
        }

        /// <summary>
        /// ctor: Used to allow the various class factories to be injected at runtime.
        /// </summary>
        /// <param name="ArtistFactory">Injected instance of the ArtistFactory</param>
        /// <param name="AlbumFactory">Injected instance of the AlbumFactory</param>
        /// <param name="TrackFactory">Injected instance of the TrackFactory</param>
        /// <param name="ImageFactory">Injected instance of the ImageFactory</param>
        /// <param name="GenreFactory">Injected instance of the GenreFactory</param>
        /// <param name="PlaylistFactory">Injected instance of the PlaylistFactory</param>
        /// <param name="WikiFactory">Injected instance of the WikiFactory</param>
        /// <remarks>
        /// <para>
        /// It is decorated with the <c>[PreferredConstructor]</c> attribute, which 
        /// belongs to the MVVMLight framework.  It determines which constructor the
        /// MVVMLight SimpleIoC container should use when instantiating the class.
        /// It is required, because the XMLSerialisation process, used to persist 
        /// this model, requires a parameterless Constructor.
        /// </para>
        /// <para>
        /// The instances of the various factories are required by the 
        /// <c>InitialiseMusiccollection</c> methods.
        /// </para>
        /// </remarks>
        [PreferredConstructor]
        public MusicCollection(AbstractFactory<Artist> ArtistFactory, 
            AbstractFactory<Album> AlbumFactory, 
            AbstractFactory<Track> TrackFactory, 
            AbstractFactory<Image> ImageFactory,
            AbstractFactory<Genre> GenreFactory,
            AbstractFactory<PlayList> PlaylistFactory,
            AbstractFactory<Wiki> WikiFactory)
        {
            //  Load the dependencies
            if (ArtistFactory == null)
                throw new ArgumentNullException("ArtistFactory", "No valid Factory supplied to create Artist class");
            _artistFactory = ArtistFactory;
            if (AlbumFactory == null)
                throw new ArgumentNullException("AlbumFactory", "No valid Factory supplied to create Album class");
            _albumFactory = AlbumFactory;
            if (TrackFactory == null)
                throw new ArgumentNullException("TrackFactory", "No valid Factory supplied to create Track class");
            _trackFactory = TrackFactory;
            if (ImageFactory == null)
                throw new ArgumentNullException("ImageFactory", "No valid Factory supplied to create Image class");
            _imageFactory = ImageFactory;
            if (GenreFactory == null)
                throw new ArgumentNullException("GenreFactory", "No valid Factory supplied to create Genre class");
            _genreFactory = GenreFactory;
            if (PlaylistFactory == null)
                throw new ArgumentNullException("PlaylistFactory", "No valid Factory supplied to create Playlist class");
            _playlistFactory = PlaylistFactory;
            if (WikiFactory == null)
                throw new ArgumentNullException("WikiFactory", "No valid Factory supplied to create Wiki class");
            _wikiFactory = WikiFactory;

            //  TODO: Refactor the factories to be injected directly into the InitialiseMusiccollection
                
            //  Initialise the properties / collection
            this.Albums = new List<Album>();
            this.Tracks = new List<Track>();
            this.Artists = new List<Artist>();
            this.Genres = new List<Genre>();
            this.PlayLists = new List<PlayList>();

        }


        //  TODO: Replace the List<T> holding the mucis collection with a Concurrent version to support mutli threading
        /// <summary>
        /// The collostion of Albums making up the Collection
        /// </summary>
        /// <remarks>
        /// Replace the List<T> holding the mucis collection with a Concurrent version to support mutli threading
        /// 
        ///     NB: This could be the ConcurrentBag or the ConcurrentDictiionary.  Whichever is used MUST meed the following
        ///         Criteri:
        ///         1.  Must support the Add method to add an item
        ///         2.  Must support enumarations over the collection
        ///         3.  Must support the use of Linq-To-Objects to retrieve information
        ///         4.  Must support the ability to delete the item
        ///         5.  Must support the ability to replace/update an item.  this can be Remove the item, update and 
        ///             add back.
        ///         This is needed because the collection have to be thread-safe and support the various locking
        ///         options available to ensure safe update of items.  Even though this is a single user application
        ///         the async model is used throughout the appliation to keep the UI responsive, but means that the
        ///         possiblity exists that updates are concurrently performed from different threads.
        ///
        /// </remarks>
        public List<Album> Albums {get; set;}

        /// <summary>
        /// The Tracks belonging to the Aobums in the Collection
        /// </summary>
        public List<Track> Tracks { get; set; }

        /// <summary>
        /// The Artists that have albums in the collection
        /// </summary>
        public List<Artist> Artists { get; set; }

        /// <summary>
        /// The various Genres associated with album in the collection
        /// </summary>
        public List<Genre> Genres { get; set; }

        /// <summary>
        /// The various playlist containing albums and tracks from the collection
        /// </summary>
        public List<PlayList> PlayLists { get; set; }



        /// <summary>
        /// Adds a new album to the In-Memory context, updating the related entities.
        /// </summary>
        /// <param name="newAlbum">The Album to be Created and added to the context</param>
        /// <returns>The instance of the album created</returns>
        public async Task<Album> CreateAlbum(Album newAlbum)
        {
            //  1.  Calculate the new AlbumId and update the album
            newAlbum.Id = _helper.GenerateAlbumId(this);

            //  2.  Add the album ref to each track and add each to the Tracks collection
            //      and update the tracks collection to ensure the navigation properties are updated.
            var updatedTracks = _helper.AddTracksToContext(this, newAlbum.Tracks, newAlbum);
            newAlbum.Tracks = updatedTracks;

            //  3   Add each genre to the Genres collection, ensure navigation properties are updated
            var updatedGenres = _helper.AddGenresToContext(this, newAlbum.Genres, newAlbum);
            newAlbum.Genres = updatedGenres;

            //  4   Add the Artist to the Artist collection, ensure navigation properties are updated.
            var updatedArtist = _helper.AddArtistToContext(this, newAlbum.Artist, newAlbum);
            newAlbum.Artist = updatedArtist;

            //  5.  Add the album to the Albums collection
            this.Albums.Add(newAlbum);

            return newAlbum;
        }


        /// <summary>
        /// Gets or sets the reference to the StorageFile used for persisting the data within the model.
        /// </summary>
        /// <remarks>
        /// The instance of the storageFile must be kept precisely as long as the instance of the
        /// In-memory context is required.
        /// </remarks>
        public StorageFile PersistenceFile
        {
            get { return _dataFile; }
            set 
            {
                if (_dataFile != value && value != null)                
                    _dataFile = value; 
            }
        }

    }
}
